using BudgetManagementAPI.Entities.Context;
using BudgetManagementAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Validations;

namespace BudgetManagementAPI.BgService;

public class BudgetBgService : BackgroundService
{
    private readonly IServiceScopeFactory _providerFactory;

    public BudgetBgService(IServiceScopeFactory providerFactory)
    {
        _providerFactory = providerFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (DateTime.UtcNow.Day != 1)
            {
                await Task.Delay(60000, stoppingToken); // Rulează la fiecare 60 de secunde
            }

            using var scope = _providerFactory.CreateScope();

            var uoW = scope.ServiceProvider.GetRequiredService<UnitOfWork>();
            
            var budgets = await uoW.Budgets.ToListAsync();

            foreach (var budget in budgets)
            {
               budget.TotalPercentageSpent = 0; 
            }
            await uoW.SaveChangesAsync();   
        }
    }
}

