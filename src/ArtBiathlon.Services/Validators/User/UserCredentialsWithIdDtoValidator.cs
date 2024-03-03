using ArtBiathlon.Domain.Models;
using ArtBiathlon.Domain.Models.User.UserCredential;
using FluentValidation;

namespace ArtBiathlon.Services.Validators.User;

public class UserCredentialsWithIdDtoValidator : AbstractValidator<ModelDtoWithId<UserDto>>
{
    public UserCredentialsWithIdDtoValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.Model)
            .SetValidator(new UserCredentialsDtoValidator());
    }
}