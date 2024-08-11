using AutoMapper;
using BudgetManagementAPI.Context;
using BudgetManagementAPI.Entities;
using BudgetManagementAPI.Models.Lesson;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BudgetManagementAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LevelLessonController : ControllerBase
{
    private readonly UnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public LevelLessonController(UnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllLevelLesson ()
    {
        var levels =  _unitOfWork.LevelLessons.ToList(); 
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

