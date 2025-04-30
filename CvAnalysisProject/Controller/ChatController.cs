using CvAnalysisSystem.DAL.SqlServer.Context;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CvAnalysisSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ChatController(AppDbContext context)
        {
            _context = context;
        }

        // 🧠 Operator panel üçün istifadəçilərin siyahısı
        [HttpGet("users")]
        public IActionResult GetChatUsers()
        {
            var userIds = _context.ChatMessages
                .Select(x => x.SenderId)
                .Where(id => id != "operator" && id != "bot")
                .Distinct()
                .ToList();

            return Ok(userIds);
        }

        // 📜 Müəyyən istifadəçinin bütün chat tarixçəsi
        [HttpGet("history/{userId}")]
        public IActionResult GetChatHistory(string userId)
        {
            var messages = _context.ChatMessages
                .Where(m => m.SenderId == userId || m.ReceiverId == userId)
                .OrderBy(m => m.Timestamp)
                .Select(m => new
                {
                    sender = m.SenderId == "operator" ? "Operator" :
                             m.SenderId == userId ? "Siz" : "Bot",
                    message = m.Message,
                    timestamp = m.Timestamp
                })
                .ToList();

            return Ok(messages);
        }
    }
}
