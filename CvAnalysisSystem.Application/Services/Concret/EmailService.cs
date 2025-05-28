using CvAnalysisSystem.Application.CQRS.Email;
using CvAnalysisSystem.Application.Services.Interfaces;
using CvAnalysisSystem.Common.Email;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace CvAnalysisSystem.Application.Services.Implementations;

public class EmailService : IEmailService
{
    private readonly EmailSettings _emailSettings;

    public EmailService(IOptions<EmailSettings> emailSettings)
    {
        _emailSettings = emailSettings.Value;
    }

    public async Task SendEmailAsync(EmailRequestDto request)
    {
        if (string.IsNullOrWhiteSpace(request.ToEmail))
            throw new ArgumentNullException(nameof(request.ToEmail), "Email ünvanı boş ola bilməz");

        var message = new MailMessage
        {
            From = new MailAddress(_emailSettings.From),
            Subject = request.Subject,
            Body = request.Body,
            IsBodyHtml = true
        };

        message.To.Add(new MailAddress(request.ToEmail));

        using var client = new SmtpClient(_emailSettings.Host, _emailSettings.Port)
        {
            Credentials = new NetworkCredential(_emailSettings.Username, _emailSettings.Password),
            EnableSsl = true
        };

        await client.SendMailAsync(message);
    }
}

