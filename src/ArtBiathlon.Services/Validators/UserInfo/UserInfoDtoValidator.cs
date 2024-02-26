using ArtBiathlon.Domain.Models.User.UserInfo;
using FluentValidation;

namespace ArtBiathlon.Services.Validators.UserInfo;

public class UserInfoDtoValidator : AbstractValidator<UserInfoDto>
{
    public UserInfoDtoValidator()
    {
        RuleFor(x => x.Gender)
            .NotNull();

        RuleFor(x => x.Surname.Length)
            .NotNull()
            .LessThanOrEqualTo(50);

        RuleFor(x => x.Name.Length)
            .NotNull()
            .LessThanOrEqualTo(50);

        RuleFor(x => x.MiddleName!.Length)
            .LessThanOrEqualTo(50)
            .When(x => true);

        RuleFor(x => x.BirthDate)
            .NotNull()
            .GreaterThanOrEqualTo(DateTimeOffset.Now.AddYears(-100))
            .LessThanOrEqualTo(DateTimeOffset.Now.AddYears(-10));

        RuleFor(x => x.Status)
            .NotNull();

        RuleFor(x => x.Rank)
            .NotNull();

        RuleFor(x => x.UserAvatar.Length)
            .GreaterThanOrEqualTo(0)
            .When(x => true);

        RuleFor(x => x.Email)
            .NotNull()
            .EmailAddress()
            .WithMessage("Не соответствует типу адреса электронной почты");
    }
}