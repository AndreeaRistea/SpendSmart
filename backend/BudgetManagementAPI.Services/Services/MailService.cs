using System.Net;
using System.Net.Mail;
using BudgetManagementAPI.Services.Interfaces;
using BudgetManagementAPI.Services.Utils;
using Microsoft.Extensions.Options;

namespace BudgetManagementAPI.Services.Services;

public class MailService : IMailService
{
    private readonly MailSettings _settings;
    public MailService(IOptions<MailSettings> settings)
    {
        _settings = settings.Value;
    }
    public async Task SendEmailAsync(MailMessage email)
    {
        using var smtpClient = new SmtpClient
        {
            EnableSsl = false,
            UseDefaultCredentials = false,
            Host = _settings.Host,
            Port = _settings.Port,
            Credentials = new NetworkCredential
            {
                UserName = _settings.Mail,
                Password = _settings.AppPassword
            }, 
        };

         email.From = new MailAddress(_settings.Mail);

        await smtpClient.SendMailAsync(email);
    }
}

