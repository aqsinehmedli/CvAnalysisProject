using Microsoft.AspNetCore.Http;

namespace CvAnalysisSystem.Application.Services
{
    public interface IPdfReaderService
    {
        Task<string> ReadPdfAsync(IFormFile file);
    }
}
