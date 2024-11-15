using BudgetManagementAPI.Entities.Context;
using BudgetManagementAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BudgetManagementAPI.BgService;

public class BudgetBgService : BackgroundService
{
    private readonly ILogger<BudgetBgService> _logger;
    private readonly ILessonService _lessonService;
    private readonly UnitOfWork _unitOfWork;

    public BudgetBgService(ILogger<BudgetBgService> logger, ILessonService lessonService, UnitOfWork unitOfWork)
    {
        _logger = logger;
        _lessonService = lessonService;
        _unitOfWork = unitOfWork;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

            var users = await _unitOfWork.Users.ToListAsync();

            foreach (var user in users)
            {
                await _lessonService.AddLessonsToUserAsync(user.Id);
            }

            await Task.Delay(60000, stoppingToken); // Rulează la fiecare 60 de secunde
        }
    }
}

