using FluentValidation;
using CvAnalysisSystem.Application.CQRS.Users.Handlers.Commands;

public class RegisterValidator : AbstractValidator<Register.Command>
{
    public RegisterValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Ad boş ola bilməz")
            .MinimumLength(3).WithMessage("Ad ən azı 3 simvol olmalıdır");

        RuleFor(x => x.Surname)
            .NotEmpty().WithMessage("Soyad boş ola bilməz")
            .MinimumLength(3).WithMessage("Soyad ən azı 3 simvol olmalıdır");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email boş ola bilməz")
            .Matches(@"^[^@]+@[^@]+\.[^@]+$").WithMessage("Düzgün email daxil edin");

        RuleFor(x => x.MobilePhone)
            .NotEmpty().WithMessage("Mobil nömrə boş ola bilməz")
            .Matches(@"^\+994\d{9}$").WithMessage("Mobil nömrə formatı düzgün deyil (+994XXXXXXXXX)");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Şifrə boş ola bilməz")
            .MinimumLength(6).WithMessage("Şifrə ən azı 6 simvol olmalıdır");


        RuleFor(x => x.Gender)
            .NotNull().WithMessage("Cins seçilməlidir");

        RuleFor(x => x.BirthDate)
            .NotNull().WithMessage("Doğum tarixi boş ola bilməz")
            .LessThan(DateTime.Now).WithMessage("Doğum tarixi gələcək ola bilməz");
    }
}
