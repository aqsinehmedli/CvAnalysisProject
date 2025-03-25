using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static CvAnalysisSystem.Application.CQRS.Users.Handlers.Commands.Register;

namespace CvAnalysisSystemProject.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] Command request)
        {
            return Ok(await Sender.Send(request));
        }
    }
}
