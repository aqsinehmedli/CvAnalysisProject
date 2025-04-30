namespace CvAnalysisSystem.Application.Services
{
    public static class ChatBotHelper
    {
        public static string GetBotResponse(string input)
        {
            input = input.ToLower();
            if (input.Contains("product")) return "Bizim məhsullar: CV yoxlama, analiz, təkliflər...";
            if (input.Contains("service")) return "Xidmətlərimiz: PDF analizi, tərcümə, dizayn...";
            if (input.Contains("əlaqə")) return "Əlaqə üçün: contact@cvcheck.az";
            if (input.Contains("operator")) return "Operatora bağlanılır...";
            return "Sualınızı daha ətraflı yazın və ya 'operator' yazın.";
        }
    }
}
