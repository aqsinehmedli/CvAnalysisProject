namespace CvAnalysisSystem.Application.Services.Abstract
{
    public interface IEmailService
    {
        Task SendEmailAsync(string subject, string body, string toEmail);
    }
}
