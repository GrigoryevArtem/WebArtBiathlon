using ArtBiathlon.Domain.Models;
using ArtBiathlon.Domain.Models.User.UserSign;
using FluentValidation;

namespace ArtBiathlon.Services.Validators.User;

public class UserAuthenticationWithIdDtoValidator : AbstractValidator<ModelDtoWithId<UserAuthenticationDto>>
{
    public UserAuthenticationWithIdDtoValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.Model)
            .SetValidator(new UserAuthenticationDtoValidator());
    }
}