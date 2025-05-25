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


namespace CvAnalysisSystemProject.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CvController(CvService cvService) : BaseController
    {
        private readonly CvService _cvService = cvService;

        //public async Task<IActionResult> Create([FromBody] CreateCv.CvCommand request)
        //{
        //    var response = await Sender.Send(request);
        //    return Ok(response);
        //}
        [HttpPost("generate-pdf")]
        public async Task<IActionResult> GenerateCv([FromBody] CreateCv.CvCommand command)
        {
            byte[] pdfBytes = _cvService.GenerateCv(command, command.TemplateType);  // <-- BU ASYNC DEYİL

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (pdfBytes == null || pdfBytes.Length == 0)
            {
                return BadRequest("CV PDF yaratılamadı.");
            }

            var pdf = await Sender.Send(command); // <-- BURADA ƏSLİNDƏ NƏ YOLLAYIRSAN? NİYƏ LAZIMDIR BURADA?
            return File(pdfBytes, "application/pdf", "cv.pdf");
        }


        //[HttpPut]
        //public async Task<IActionResult> Update([FromBody] UpdateCv.CvCommand request)
        //{
        //    var response = await Sender.Send(request);
        //    return Ok(response);
        //}
        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            var request = new DeleteCv.CvCommand() { Id = id };
            var response = await Sender.Send(request);
            return Ok(response);
        }



        private static List<Cv> _cvs = new List<Cv>();
        private static int _idCounter = 1;

        // POST api/cv
        // CV yaratmaq və PDF faylı upload etmək üçün endpoint
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> CreateCv([FromForm] CvCreateRequest request)
        {
            if (request.PdfFile == null || request.PdfFile.Length == 0)
                return BadRequest("PDF faylı daxil edilməyib.");

            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "UploadedPdfs");
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            var fileName = $"{Guid.NewGuid()}_{request.PdfFile.FileName}";
            var filePath = Path.Combine(uploadPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await request.PdfFile.CopyToAsync(stream);
            }

            var cv = new Cv
            {
                Id = _idCounter++,
                UserId = request.UserId,
                PdfFilePath = fileName,
                Education = request.Education,
                WorkExperience = request.WorkExperience,
                Skills = request.Skills,
                Languages = request.Languages,
                Certifications = request.Certifications,
                Status = "Created",
                UploadDate = DateTime.UtcNow,
                LastUpdated = DateTime.UtcNow,
                AiAnalysis = null,
                IsDeleted = false,
                DeletedDate = DateTime.MinValue
            };

            _cvs.Add(cv);

            return Ok(cv);
        }

        // GET api/cv/{id}
        // CV məlumatlarını əldə etmək üçün endpoint
        [HttpGet("{id}")]
        public IActionResult GetCv(int id)
        {
            var cv = _cvs.Find(c => c.Id == id && !c.IsDeleted);
            if (cv == null)
                return NotFound();

            return Ok(cv);
        }

        // GET api/cv/download/{id}
        // PDF faylını yükləmək üçün endpoint
        [HttpGet("download/{id}")]
        public IActionResult DownloadPdf(int id)
        {
            var cv = _cvs.Find(c => c.Id == id && !c.IsDeleted);
            if (cv == null || string.IsNullOrEmpty(cv.PdfFilePath))
                return NotFound();

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "UploadedPdfs", cv.PdfFilePath);

            if (!System.IO.File.Exists(filePath))
                return NotFound();

            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            return File(fileBytes, "application/pdf", $"{cv.UserId}_cv.pdf");
        }
    }

    // Request model for CV creation with file upload
    public class CvCreateRequest
    {
        public int UserId { get; set; }
        public string Education { get; set; }
        public string WorkExperience { get; set; }
        public string Skills { get; set; }
        public string Languages { get; set; }
        public string Certifications { get; set; }
        public Microsoft.AspNetCore.Http.IFormFile PdfFile { get; set; }
    }
}
