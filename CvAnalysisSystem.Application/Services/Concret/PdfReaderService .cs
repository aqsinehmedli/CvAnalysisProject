using System.Text;
using Microsoft.AspNetCore.Http;
using UglyToad.PdfPig;

namespace CvAnalysisSystem.Application.Services
{
    public class PdfReaderService : IPdfReaderService
    {
        public async Task<string> ReadPdfAsync(IFormFile file)
        {
            using var stream = file.OpenReadStream();
            using var pdf = PdfDocument.Open(stream);
            var text = new StringBuilder();

            foreach (var page in pdf.GetPages())
            {
                text.AppendLine(page.Text);
            }

            return text.ToString();
        }

      

        
    }
}
