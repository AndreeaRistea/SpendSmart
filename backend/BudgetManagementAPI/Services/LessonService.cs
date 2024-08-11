using AutoMapper;
using BudgetManagementAPI.Context;
using BudgetManagementAPI.Entities;
using BudgetManagementAPI.Enums;
using BudgetManagementAPI.Interfaces;
using BudgetManagementAPI.Migrations;
using BudgetManagementAPI.Models.Lesson;
using Microsoft.EntityFrameworkCore;

namespace BudgetManagementAPI.Services;

public class LessonService : ILessonService
{
    private readonly UnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public LessonService(UnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<LessonDto> CreateLessonAsync(LessonDto lesson)
    {
        var levelLesson = await _unitOfWork.LevelLessons.FirstOrDefaultAsync(l => l.LevelLessonId == lesson.LevelLessonId);
        if (levelLesson == null)
        {
            throw new Exception("Nu există nivelul.");
        }

        if (lesson.CoverImageFile != null && lesson.CoverImageFile.Length > 0)
        {
            using (var ms = new MemoryStream())
            {
                await lesson.CoverImageFile.CopyToAsync(ms);
                lesson.CoverImage = ms.ToArray();
            }
        }

        if (lesson.ContentLesson != null && lesson.ContentLesson.Length > 0)
        {
            using (var ms = new MemoryStream())
            {
                await lesson.ContentLesson.CopyToAsync(ms);
                lesson.FileText = ms.ToArray();
                lesson.LessonContentName = lesson.ContentLesson.FileName;
            }
        }
        var newLesson = new Lesson
        {
            LessonId = lesson.LessonId,
            LevelLessonId = levelLesson.LevelLessonId,
            CoverImageFile = lesson.CoverImageFile,
            CoverImage = lesson.CoverImage,
            ContentLesson = lesson.ContentLesson,
            FileText = lesson.FileText,
            LessonContentName = lesson.LessonContentName,
        };

        _unitOfWork.Lessons.Add(newLesson);

        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<LessonDto>(newLesson);
    }

    public async Task<bool> AddLessonsToUserAsync(Guid userId)
    {
        var user = await _unitOfWork.Users
            .FirstOrDefaultAsync(u => u.Id == userId);

        var budgets = await _unitOfWork.Budgets
            .Where(b => b.UserId == userId)
            .ToListAsync();

        var overSpentBudgets = budgets.Where(b => b.TotalPercentageSpent > b.Percent);
        foreach ( var budget in overSpentBudgets)
        {
            var lessons = await _unitOfWork.Lessons
            .Where(l => l.Level.Level == user.LevelFinancialEducation
            && budget.TotalPercentageSpent >= l.Level.MinValue
            && budget.TotalPercentageSpent <= l.Level.MaxValue)
            .ToListAsync();

            user.Lessons = lessons;
        }

        return await _unitOfWork.SaveChangesAsync() > 0;
    }

    public async Task<List<LessonDto>> GetLessonByUserAsync(Guid userId)
    {
        var user = await _unitOfWork.Users
             .Include(u => u.Lessons)
             .ThenInclude(l => l.Level)
             .FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null)
        {
            throw new Exception("User not found.");
        }

        var lessonDtos = user.Lessons.Select(l => new LessonDto
        {
            LessonId = l.LessonId,
            LevelLessonId = l.LevelLessonId,
            CoverImage = l.CoverImage,
            FileText = l.FileText,
            LessonContentName = l.LessonContentName,
        })
            .ToList();

        return lessonDtos;
    }
}
