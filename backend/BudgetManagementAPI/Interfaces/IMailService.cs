using System.Net.Mail;

namespace BudgetManagementAPI.Interfaces;

public interface IMailService
{
    Task SendEmailAsync(MailMessage email);
}

