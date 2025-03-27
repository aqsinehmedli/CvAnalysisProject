using CvAnalysisSystem.Application.CQRS.Users.Handlers.Commands;
using FluentValidation;

namespace CvAnalysisSystem.Application.CQRS.Users.Validators
{
    public class LoginRequestValidator : AbstractValidator<Login.LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email boş ola bilmez")
                .EmailAddress().WithMessage("Yalnış email formatı");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Şifrə boş ola bilməz")
                .MinimumLength(6).WithMessage("Şifrə minimum 6 simvol olmalidir");
        }
    }
}
