using ArtBiathlon.Domain.Models.User.UserCredential;
using FluentValidation;

namespace ArtBiathlon.Services.Validators.User;

public class UserCredentialsDtoValidator : AbstractValidator<UserDto>
{
    public UserCredentialsDtoValidator()
    {
        RuleFor(x => x.Login)
            .NotNull()
            .NotEmpty()
            .Length(4, 16);
        
        
        RuleFor(x => x.PasswordHash)
            .NotNull()
            .NotEmpty();
    }
}