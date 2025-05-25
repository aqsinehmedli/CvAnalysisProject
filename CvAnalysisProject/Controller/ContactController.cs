using Microsoft.AspNetCore.Mvc;
using CvAnalysisSystem.Application.CQRS.Cv.DTOs;
using CvAnalysisSystem.Application.Services.Abstract;

namespace CvAnalysisSystemProject.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public ContactController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost]
        public async Task<IActionResult> SendContactMessage([FromBody] ContactFormDto form)
        {
            try
            {
                string subject = $"Yeni əlaqə mesajı: {form.FullName}";
                string body = $"Ad Soyad: {form.FullName}\nEmail: {form.Email}\n\nMesaj:\n{form.Message}";
                string toEmail = "support@example.com";

                await _emailService.SendEmailAsync(subject, body, toEmail);

                return Ok(new { success = true, message = "Mesaj göndərildi." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = "Email göndərilə bilmədi.", error = ex.Message });
            }
        }
    }
}
