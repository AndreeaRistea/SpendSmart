using System.Net.Mail;

namespace BudgetManagementAPI.Services.Interfaces;

public interface IMailService
{
    Task SendEmailAsync(MailMessage email);
}

