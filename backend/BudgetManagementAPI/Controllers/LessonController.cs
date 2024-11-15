using BudgetManagementApi.Dtos.Models.Lesson;
using BudgetManagementAPI.Helpers;
using BudgetManagementAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BudgetManagementAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LessonController : ControllerBase
{
    private readonly ILessonService _lessonService;
    private readonly CurrentUser _currentUser;
    public LessonController(CurrentUser currentUser, ILessonService lessonService)
    {
        _currentUser = currentUser;
        _lessonService = lessonService;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetUserLesson()
    {
        var userId = _currentUser.Id;

        var budgets = await _lessonService.GetLessonByUserAsync(userId);

        return Ok(budgets);
    }

    [HttpPost]
    public async Task<IActionResult> CreateLesson (LessonDto lesson)
    {
        var lessonAdded = await _lessonService.CreateLessonAsync(lesson);

        return Ok(lessonAdded);
    }

    [HttpGet("add-user-lesson")]
    [Authorize] 
    public async Task<IActionResult> AddUserLesson ()
    {
        var userId = _currentUser.Id;
        var lesson = await _lessonService.AddLessonsToUserAsync(userId);

        return Ok(lesson);
    }
}

