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