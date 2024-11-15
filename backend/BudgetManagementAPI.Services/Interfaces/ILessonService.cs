﻿using BudgetManagementApi.Dtos.Models.Lesson;

namespace BudgetManagementAPI.Services.Interfaces;

public interface ILessonService
{
    Task<LessonDto> CreateLessonAsync(LessonDto lesson);
    Task<bool> AddLessonsToUserAsync(Guid userId);
    Task<List<LessonDto>> GetLessonByUserAsync(Guid userId);
}

