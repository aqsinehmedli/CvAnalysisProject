using CvAnalysisSystem.Application.CQRS.Email;
using System.Threading.Tasks;

namespace CvAnalysisSystem.Application.Services.Interfaces;

public interface IEmailService
{
    Task SendEmailAsync(EmailRequestDto request);
}