using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CvAnalysisSystem.Application.CQRS.Cv.Handlers.Commands;
using CvAnalysisSystem.Application.CQRS.Cv.DTOs;
using CvAnalysisSystem.Application.Services.Abstract;
using CvAnalysisSystem.Domain.Entities;
using CvAnalysisSystem.Application.Services.Concret;
using AutoMapper;
using MediatR;
using CvAnalysisSystem.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using CvAnalysisSystem.DAL.SqlServer.Context;


namespace CvAnalysisSystemProject.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CvController(CvService cvService, AppDbContext context) : BaseController
    {
        private readonly CvService _cvService = cvService;
        private readonly AppDbContext _context = context;



        //public async Task<IActionResult> Create([FromBody] CreateCv.CvCommand request)
        //{
        //    var response = await Sender.Send(request);
        //    return Ok(response);
        //}
        [HttpPost]
        public async Task<IActionResult> CreateCv([FromBody] CvModel cvModel)
        {
            if (cvModel == null)
            {
                return BadRequest("Geçersiz CV verisi.");
            }

            // Veritabanına ekleme işlemi
            _context.CvModel.Add(cvModel);
            await _context.SaveChangesAsync();

            return Ok(new { message = "CV başarıyla kaydedildi!" });
        }
    }
    //[HttpPost("generate-pdf")]
    //public async Task<IActionResult> GenerateCv([FromBody] CreateCv.CvCommand command)
    //{
    //    byte[] pdfBytes = _cvService.GenerateCv(command, command.TemplateType);  // TemplateType artık command'in içinde

    //    if (!ModelState.IsValid)
    //        return BadRequest(ModelState);

    //    if (pdfBytes == null || pdfBytes.Length == 0)
    //    {
    //        return BadRequest("CV PDF yaratılamadı.");
    //    }

    //    var pdf = await Sender.Send(command);
    //    return File(pdfBytes, "application/pdf", "cv.pdf");
    //}



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
