using FluentValidation;
using CvAnalysisSystem.Application.CQRS.Skills.Commands;

namespace CvAnalysisSystem.Application.CQRS.Skills.Validators;

public class UpdateSkillValidator : AbstractValidator<UpdateSkillCommand>
{
    public UpdateSkillValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Valid Skill ID is required.");

        RuleFor(x => x.SkillName)
            .NotEmpty().WithMessage("Skill name cannot be empty.")
            .MaximumLength(255).WithMessage("Skill name must not exceed 255 characters.");

        RuleFor(x => x.ProficiencyLevel)
            .NotEmpty().WithMessage("Proficiency level is required.")
            .MaximumLength(50).WithMessage("Proficiency level must not exceed 50 characters.");

        RuleFor(x => x.StartYear)
            .InclusiveBetween(1900, 2100).WithMessage("Start year must be between 1900 and 2100.");

        RuleFor(x => x.EndYear)
            .InclusiveBetween(1900, 2100).WithMessage("End year must be between 1900 and 2100.")
            .GreaterThanOrEqualTo(x => x.StartYear).WithMessage("End year cannot be earlier than start year.");

        RuleFor(x => x.UpdatedBy)
            .NotEmpty().WithMessage("UpdatedBy is required.");

        RuleFor(x => x.UpdatedDate)
            .NotEmpty().WithMessage("Updated date is required.");
    }
}
