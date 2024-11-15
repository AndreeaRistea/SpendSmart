using BudgetManagementAPI.Entities.Context;
using BudgetManagementAPI.Services.Interfaces;
using BudgetManagementAPI.Services.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BudgetManagementAPI.BgService;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {

                services.AddDbContext<UnitOfWork>(
                    optionsBuilder =>
                        optionsBuilder.UseNpgsql(hostContext.Configuration.GetConnectionString("BudgetManagementDb"))
                        );

                services.AddScoped<ILessonService, LessonService>();
                services.AddScoped<UnitOfWork>();
                services.AddAutoMapper(typeof(Program));

                services.AddHostedService<BudgetBgService>();
            });
}

