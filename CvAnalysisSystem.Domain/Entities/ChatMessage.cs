using System;

namespace CvAnalysisSystem.Domain.Entities
{
    public class ChatMessage
    {
        public int Id { get; set; }                    // Primary key üçün
        public string SenderId { get; set; } = null!;  // Göndərənin userId-si
        public string ReceiverId { get; set; } = null!; // Alanın userId-si (operator və ya user)
        public string Message { get; set; } = null!;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
