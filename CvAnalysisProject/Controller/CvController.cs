using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CvAnalysisSystem.Application.CQRS.Cv.Handlers.Commands;
using CvAnalysisSystem.Application.CQRS.Cv.DTOs;
using CvAnalysisSystem.Application.Services.Abstract;
using CvAnalysisSystem.Application.Services.Concret;
using CvAnalysisSystem.Domain.Entities;
using AutoMapper;

namespace CvAnalysisSystemProject.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CvController(IPDFService pdfService) : BaseController
    {
        private readonly IPDFService _pdfService = pdfService;

      
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCv.CvCommand request)
        {
            var response = await Sender.Send(request);
            return Ok(response);
        }
        [HttpPost("generate-pdf")]
        public IActionResult GenerateCvPdf([FromBody] CvModel model)
        {
            try
            {
                var pdfBytes = _pdfService.GenerateCvPdf(model);
                return File(pdfBytes, "application/pdf", "CvFile.pdf");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error generating PDF: {ex.Message}");
            }
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
