using CvAnalysisSystem.Application.CQRS.Cv.Handlers.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvAnalysisSystem.Application.CQRS.Cv.Validators
{
    public class CreateCvRequestValidator:AbstractValidator<CreateCv.CvCommand>
    {
        public CreateCvRequestValidator() {
            RuleFor(x => x.UserId).GreaterThan(0).WithMessage("UserId must be greater than 0.");
            RuleFor(x => x.PdfFilePath)
                .NotEmpty().WithMessage("PDF file path is required.")
                .Matches(@"^.*\.pdf$").WithMessage("Only PDF files are allowed.");
            RuleFor(x => x.Education).NotEmpty().WithMessage("Education information is required.");
            RuleFor(x => x.WorkExperience).NotEmpty().WithMessage("Work experience is required.");
            RuleFor(x => x.Skills).NotEmpty().WithMessage("Skills are required.");
            RuleFor(x => x.Languages).NotEmpty().WithMessage("Languages are required.");
            RuleFor(x => x.Certifications).NotEmpty().WithMessage("Certifications are required.");
            RuleFor(x => x.Status)
                .Must(status => new[] { "Active", "Inactive", "Pending" }.Contains(status))
                .WithMessage("Status must be one of the following: Active, Inactive, or Pending.");
            RuleFor(x => x.AiAnalysis).NotEmpty().WithMessage("AI analysis is required.");
            RuleFor(x => x.DeletedDate).GreaterThan(DateTime.MinValue).WithMessage("Invalid deleted date.");
        }
    }
}
