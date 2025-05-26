using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using CvAnalysisSystem.Application.Services.Abstract;
using Microsoft.Extensions.Logging;

namespace CvAnalysisSystem.Application.Services.Concret
{
    public class SMTPEmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<IEmailService> _logger;
        private MailMessage message;

        public SMTPEmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string subject, string body, string toEmail)
        {
            var smtpHost = _configuration["Smtp:Host"];
            var smtpPort = int.Parse(_configuration["Smtp:Port"]);
            var smtpUser = _configuration["Smtp:Username"];
            var smtpPass = _configuration["Smtp:Password"];
            var fromEmail = _configuration["Smtp:From"];

        

            var mailMessage = new MailMessage
            {
                From = new MailAddress(fromEmail),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };

            message.To.Add(toEmail);

            using var client = new SmtpClient(smtpHost, smtpPort)
            {
                Credentials = new NetworkCredential(smtpUser, smtpPass),
                EnableSsl = true
            };

            try
            {
                await client.SendMailAsync(message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "an ocurred Error while sending email");
                throw;
            }
        }
    }
}
