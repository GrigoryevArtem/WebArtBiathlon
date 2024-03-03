using ArtBiathlon.Domain.Models.User.UserSign;
using FluentValidation;

namespace ArtBiathlon.Services.Validators.User;

public class UserAuthenticationDtoValidator : AbstractValidator<UserAuthenticationDto>
{
    public UserAuthenticationDtoValidator()
    {
        RuleFor(x => x.UserName)
            .NotNull()
            .NotEmpty()
            .Length(4, 16);

        RuleFor(x => x.PasswordHash)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.Role)
            .IsInEnum()
            .NotNull();
    }
}