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

        public OpenAiController(ILogger<OpenAiController> logger)
        {
            _logger = logger;
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
        public IActionResult Post(string question)
        {
            var credential = new ApiKeyCredential("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");
            ChatClient client = new(model: "gpt-4", credential: credential);

            var completion = client.CompleteChat("Say 'this is a test.'");

            return Ok($"{completion.Value}");
        }
    }
}
