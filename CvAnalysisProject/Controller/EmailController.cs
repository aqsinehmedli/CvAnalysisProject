using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class EmailController : ControllerBase
{
    [HttpPost("send")]
    public IActionResult SendEmail([FromBody] ContactFormModel model)
    {
        try
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("senseninemail@gmail.com")); // göndərən email
            email.To.Add(MailboxAddress.Parse("aqsinhmdli03@gmail.com"));   // qəbul edən email
            email.Subject = $"Yeni mesaj {model.FullName} tərəfindən";
            email.Body = new TextPart(MimeKit.Text.TextFormat.Plain)
            {
                Text = $"Ad: {model.FullName}\nEmail: {model.Email}\nMesaj: {model.Message}"
            };

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate("senseninemail@gmail.com", "gmail_app_password"); // Gmail app parolu
            smtp.Send(email);
            smtp.Disconnect(true);

            return Ok(new { message = "Email göndərildi." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Email göndərilə bilmədi.", error = ex.Message });
        }
    }
}

public class ContactFormModel
{
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Message { get; set; }
}
