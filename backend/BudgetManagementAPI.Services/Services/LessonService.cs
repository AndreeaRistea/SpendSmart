using AutoMapper;
using BudgetManagementApi.Dtos.Models.Lesson;
using BudgetManagementAPI.Entities.Context;
using BudgetManagementAPI.Entities.Entities;
using BudgetManagementAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BudgetManagementAPI.Services.Services;

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
            throw new KeyNotFoundException("Level doesn't exist");
        }

        if (lesson.CoverImageFile != null && lesson.CoverImageFile.Length > 0)
        {
            using (var ms = new MemoryStream())
            {
                await lesson.CoverImageFile.CopyToAsync(ms);
                lesson.CoverImage = ms.ToArray();
            }
        }

        if (lesson.CoverImageFile==null)
        {
            throw new KeyNotFoundException("Cover Image File doesn t exist");
        }

        if (lesson.ContentLesson == null)
        {
            throw new KeyNotFoundException("Content lesson doesn t exist");
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
            ContentLesson = lesson.ContentLesson!,
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
        if (user == null)
        {
            throw new KeyNotFoundException("User doesn t exist");
        }
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
            throw new KeyNotFoundException("User not found.");
        }

        if (user.Lessons == null)
        {
            throw new KeyNotFoundException("Lesson doesn t exist");
        }

        var lessonDtos = user.Lessons.Select(l => new LessonDto
        {
            LessonId = l.LessonId,
            LevelLessonId = l.LevelLessonId,
            CoverImage = l.CoverImage,
            FileText = l.FileText,
            LessonContentName = l.LessonContentName!,
            ContentLesson = l.ContentLesson,
            CoverImageFile = l.CoverImageFile
        })
            .ToList();

        return lessonDtos;
    }
}
