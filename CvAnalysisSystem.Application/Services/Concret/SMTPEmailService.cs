using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using CvAnalysisSystem.Application.Services.Abstract;

namespace CvAnalysisSystem.Application.Services.Concret
{
    public class SMTPEmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public SMTPEmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string subject, string body, string toEmail)
        {
            // Konfiqurasiya faylından məlumatlar
            var fromEmail = _configuration["EmailSettings:FromEmail"];
            var appPassword = _configuration["EmailSettings:AppPassword"];

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromEmail, appPassword),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(fromEmail),
                Subject = subject,
                Body = body,
                IsBodyHtml = false,
            };

            mailMessage.To.Add(toEmail);

            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}
