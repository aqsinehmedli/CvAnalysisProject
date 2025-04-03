using CvAnalysisSystem.Application.CQRS.Cv.Handlers.Commands;
using FluentValidation;

namespace CvAnalysisSystem.Application.CQRS.Cv.Validators
{
    public class DeleteCvRequestValidator : AbstractValidator<DeleteCv.CvCommand>
    {
        public DeleteCvRequestValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id must be greater than 0.");
        }
    }
}
