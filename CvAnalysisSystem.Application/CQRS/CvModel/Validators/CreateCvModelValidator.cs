using FluentValidation;
using CvAnalysisSystem.Application.CQRS.CvModels.Commands;

namespace CvAnalysisSystem.Application.CQRS.CvModels.Validators;

public class CreateCvModelValidator : AbstractValidator<CreateCvModelCommand>
{
    public CreateCvModelValidator()
    {
        RuleFor(x => x.UserId)
            .GreaterThan(0).WithMessage("User selection is required.");

        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("Full name cannot be empty.")
            .MaximumLength(255).WithMessage("Full name must not exceed 255 characters.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Please enter a valid email address (e.g. example@mail.com).");

        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("Phone number is required.")
            .MaximumLength(20).WithMessage("Phone number must not exceed 20 characters.");

        RuleFor(x => x.LinkedInUrl)
            .MaximumLength(255).WithMessage("LinkedIn URL must not exceed 255 characters.");

        RuleFor(x => x.GitHubUrl)
            .MaximumLength(255).WithMessage("GitHub URL must not exceed 255 characters.");

        RuleFor(x => x.TemplateName)
            .MaximumLength(100).WithMessage("Template name must not exceed 100 characters.");

        RuleFor(x => x.CreatedBy)
            .NotEmpty().WithMessage("CreatedBy field is required.");

        RuleFor(x => x.CreatedDate)
            .NotEmpty().WithMessage("Created date is required.");

        RuleFor(x => x.IsDeleted)
            .NotNull().WithMessage("IsDeleted field must be specified.");
    }
}
