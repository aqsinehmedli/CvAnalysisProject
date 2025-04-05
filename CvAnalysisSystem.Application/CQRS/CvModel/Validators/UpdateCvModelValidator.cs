using FluentValidation;
using CvAnalysisSystem.Application.CQRS.CvModels.Commands;

namespace CvAnalysisSystem.Application.CQRS.CvModels.Validators;

public class UpdateCvModelValidator : AbstractValidator<UpdateCvModelCommand>
{
    public UpdateCvModelValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("A valid CV ID is required.");

        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("Full name cannot be empty.")
            .MaximumLength(255).WithMessage("Full name must not exceed 255 characters.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Please enter a valid email address (e.g. name@example.com).");

        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("Phone number is required.")
            .MaximumLength(20).WithMessage("Phone number must not exceed 20 characters.");

        RuleFor(x => x.LinkedInUrl)
            .MaximumLength(255).WithMessage("LinkedIn URL must not exceed 255 characters.");

        RuleFor(x => x.GitHubUrl)
            .MaximumLength(255).WithMessage("GitHub URL must not exceed 255 characters.");

        RuleFor(x => x.TemplateName)
            .MaximumLength(100).WithMessage("Template name must not exceed 100 characters.");

        RuleFor(x => x.UpdatedBy)
            .NotEmpty().WithMessage("UpdatedBy field is required.");

        RuleFor(x => x.UpdatedDate)
            .NotEmpty().WithMessage("Updated date is required.");
    }
}
