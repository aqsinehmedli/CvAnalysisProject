using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static CvAnalysisSystem.Application.CQRS.Users.Handlers.Commands.Login;
using static CvAnalysisSystem.Application.CQRS.Users.Handlers.Commands.RefreshToken;
using static CvAnalysisSystem.Application.CQRS.Users.Handlers.Commands.Register;

namespace CvAnalysisSystemProject.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] Command request)
        {
            return Ok(await Sender.Send(request));
        }
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            return Ok(await Sender.Send(request));
        }

        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenReuqest request)
        {
            return Ok(await Sender.Send(request));
        }
        //[HttpPut]
        //public async Task<IActionResult> Update([FromBody] CvAnalysisSystem.Application.CQRS.Users.Handlers.Commands.Update.Command request)
        //{
        //    return Ok(await Sender.Send(request));
        //}
        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            var request = new CvAnalysisSystem.Application.CQRS.Users.Handlers.Commands.Delete.Command() { Id = id };
            return Ok(await Sender.Send(request));
        }
    }
}