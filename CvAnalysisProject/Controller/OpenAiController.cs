using CvAnalysisSystem.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenAI.Chat;
using System.ClientModel;

namespace CvAnalysisSystemProject.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class OpenAiController : ControllerBase
    {
        private readonly ILogger<OpenAiController> _logger;

        private readonly IPdfReaderService _pdfReader;
        public OpenAiController(ILogger<OpenAiController> logger, IPdfReaderService pdfReader)
        {
            _logger = logger;
            _pdfReader = pdfReader;
        }

        [HttpGet(Name = "GetOpenAiInfo")]
        public IActionResult Get()
        {
            var credential = new ApiKeyCredential("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");
            ChatClient client = new(model: "gpt-4", credential: credential);

            var completion = client.CompleteChat("Say 'this is a test.'");

            return Ok($"{completion.Value}");
        }

        [HttpPost(Name = "AskGpt")]
        public async Task<IActionResult> Post([FromForm] dto dto)
        {
            if (dto.File == null || dto.File.Length == 0)
                return BadRequest("Zəhmət olmasa bir PDF faylı yükləyin.");

            string fileContent = await _pdfReader.ReadPdfAsync(dto.File);

            if (string.IsNullOrWhiteSpace(fileContent))
                return BadRequest("PDF faylından heç bir mətn oxunmadı.");

            var credential = new ApiKeyCredential("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");
            ChatClient client = new(model: "gpt-4", credential: credential);

            string defaultPrompt = "Check my CV and write score from 100 and write what I should change for better.";
            string userQuestion = string.IsNullOrWhiteSpace(dto.Question) ? defaultPrompt : dto.Question;

            string finalPrompt = $"CV Məzmunu:\n{fileContent}\n\nSual:\n{userQuestion}";

            var completion = client.CompleteChat(finalPrompt);

            return Ok(completion.Value.Content[0].Text);
        }


    }
}