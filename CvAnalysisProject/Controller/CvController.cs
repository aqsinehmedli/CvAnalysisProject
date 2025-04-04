using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CvAnalysisSystem.Application.CQRS.Cv.Handlers.Commands;
using CvAnalysisSystem.Application.CQRS.Cv.DTOs;

namespace CvAnalysisSystemProject.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CvController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCv.CvCommand request)
        {
            var response = await Sender.Send(request);
            return Ok(response);
        }

        //[HttpPut]
        //public async Task<IActionResult> Update([FromBody] UpdateCv.CvCommand request)
        //{
        //    var response = await Sender.Send(request);
        //    return Ok(response);
        //}

        //[HttpDelete]
        //public async Task<IActionResult> Delete([FromQuery] int id)
        //{
        //    var request = new DeleteCv.CvCommand() { Id = id };
        //    var response = await Sender.Send(request);
        //    return Ok(response); 
        //}



    }
}
