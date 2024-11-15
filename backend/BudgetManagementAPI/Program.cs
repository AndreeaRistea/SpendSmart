using BudgetManagementAPI.Helpers;
using BudgetManagementAPI.Services;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using System.Text;
using BudgetManagementAPI.Entities.Context;
using BudgetManagementAPI.Services.Interfaces;
using BudgetManagementAPI.Services.Services;
using BudgetManagementAPI.Services.Utils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<UnitOfWork>(
    optionsBuilder =>
        optionsBuilder.UseNpgsql(builder.Configuration.GetConnectionString("BudgetManagementDb"))
        );

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});
builder.Services.AddHangfire(configuration => configuration
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UsePostgreSqlStorage(builder.Configuration.GetConnectionString("BudgetManagementDb")));

builder.Services.AddHangfireServer();
builder.Services.AddScoped<BudgetResetService>();


var config = builder.Configuration;

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IBudgetService, BudgetService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<ILessonService, LessonService>();
builder.Services.AddScoped<IMailService, MailService>();    
builder.Services.AddScoped<CurrentUser>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
    x =>
    {
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = config[Constants.Token.JwtIssuer],
            ValidateIssuer = true,
            ValidAudience = config[Constants.Token.JwtAudience],
            ValidateAudience = true,
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config[Constants.Token.JwtKey])),
            ValidateIssuerSigningKey = true,
        };
    });

builder.Services.AddAutoMapper(typeof(Program));
//JSON Serializer
builder.Services.AddControllers().AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore).AddNewtonsoftJson(
        options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

var app = builder.Build();

app.UseHangfireDashboard("/hangfire", new DashboardOptions
{
    Authorization = new[] { new HangfireAuthorizationFilter() } 
});

// Configurare job recurent Hangfire
RecurringJob.AddOrUpdate<BudgetResetService>(
    "ResetMonthlyBudgets",
    service => service.ResetBudgetSpentAmountsAsync(),
    Cron.Monthly(1, 0) 
);

//Enable CORS
app.UseCors(c => c.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
