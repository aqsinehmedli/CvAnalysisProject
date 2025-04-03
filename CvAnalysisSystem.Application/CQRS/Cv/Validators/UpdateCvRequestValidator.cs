using CvAnalysisSystem.Application.CQRS.Cv.Handlers.Commands;
using FluentValidation;
using System;

namespace CvAnalysisSystem.Application.CQRS.Cv.Validators
{
    public class UpdateCvRequestValidator : AbstractValidator<UpdateCv.CvCommand>
    {
        public UpdateCvRequestValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id must be greater than 0.");

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
        }
    }
}
