using ArtBiathlon.Domain.Models.User.UserSign;
using FluentValidation;

namespace ArtBiathlon.Services.Validators.User;

public class SignUpDtoValidator : AbstractValidator<SignUpDto>
{
    public SignUpDtoValidator()
    {
        RuleFor(x => x.Login)
            .NotNull()
            .NotEmpty()
            .Length(4, 16);
        
        
        RuleFor(x => x.Password)
            .NotNull()
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,25}$")
            .WithMessage("Пароль должен содержать как минимум одну заглавную букву, " +
                         "одну строчную букву, одну цифру и быть длиной не менее 8 символов" +
                         "и не более 25 символов.");
    }
}