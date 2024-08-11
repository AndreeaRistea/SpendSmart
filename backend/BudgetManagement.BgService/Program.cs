using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BudgetManagementAPI.Context;
using Microsoft.EntityFrameworkCore;
using BudgetManagementAPI.Interfaces;
using BudgetManagementAPI.Services;
using Microsoft.Extensions.Configuration;

namespace BudgetManagement.BgService;

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

