using BudgetManagementApi.Dtos.Models.Lesson;
using BudgetManagementAPI.Entities.Context;
using BudgetManagementAPI.Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BudgetManagementAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LevelLessonController : ControllerBase
{
    private readonly UnitOfWork _unitOfWork;

    public LevelLessonController(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllLevelLesson ()
    {
        var levels = await _unitOfWork.LevelLessons.ToListAsync();
        return Ok(levels);
    }

    [HttpPost]
    public async  Task<ActionResult<LevelLesson>> CreateLevelLesson (LevelLessonDto levelLesson)
    {
        levelLesson.LevelLessonId = Guid.NewGuid();
        var newLevel = new LevelLesson {
            LevelLessonId = levelLesson.LevelLessonId,
            MaxValue = levelLesson.MaxValue,
            MinValue = levelLesson.MinValue,
            Level = levelLesson.Level,
        };
        _unitOfWork.LevelLessons.Add(newLevel);
        await _unitOfWork.SaveChangesAsync();
        return Ok(newLevel);
    }
}

