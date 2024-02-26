using ArtBiathlon.Domain.Models;
using ArtBiathlon.Domain.Models.User.UserInfo;
using FluentValidation;

namespace ArtBiathlon.Services.Validators.UserInfo;

public class UserInfoWithIdDtoValidator : AbstractValidator<ModelDtoWithId<UserInfoDto>>
{
    public UserInfoWithIdDtoValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThanOrEqualTo(0);
        
        RuleFor(x => x.Model)
            .SetValidator(new UserInfoDtoValidator());
    }
    
}