using CvAnalysisSystem.Application.CQRS.Email;
using CvAnalysisSystem.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CvAnalysisSystemProject.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmailController : ControllerBase
{
    private readonly IEmailService _emailService;

    public EmailController(IEmailService emailService)
    {
        _emailService = emailService;
    }

    [HttpPost]
    public async Task<IActionResult> SendEmail([FromBody] EmailRequestDto request)
    {
        try
        {
            await _emailService.SendEmailAsync(request);
            return Ok("Email uğurla göndərildi.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Xəta baş verdi: {ex.Message}");
        }
    }
}
