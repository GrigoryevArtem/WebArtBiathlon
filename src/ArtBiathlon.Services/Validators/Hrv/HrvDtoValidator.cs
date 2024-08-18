using ArtBiathlon.Domain.Models.Hrv;
using FluentValidation;

namespace ArtBiathlon.Services.Validators.Hrv;

public class HrvDtoValidator : AbstractValidator<HrvDto>
{
    public HrvDtoValidator()
    {
        RuleFor(x => x.UserInfoId)
            .NotNull()
            .GreaterThan(0);

        RuleFor(x => x.CreatedAt)
            .NotNull()
            .GreaterThanOrEqualTo(DateTimeOffset.Now.AddYears(-5))
            .LessThanOrEqualTo(DateTimeOffset.Now);

        RuleFor(x => x.Heart)
            .NotNull()
            .GreaterThan(0);

        RuleFor(x => x.Lf)
            .NotNull()
            .GreaterThan(0);

        RuleFor(x => x.Rmssd)
            .NotNull()
            .GreaterThan(0);

        RuleFor(x => x.Rr)
            .NotNull()
            .GreaterThan(0);

        RuleFor(x => x.Sd)
            .NotNull()
            .GreaterThan(0);

        RuleFor(x => x.Hf)
            .NotNull()
            .GreaterThan(0);

        RuleFor(x => x.Readiness)
            .NotNull()
            .GreaterThan(0);

        RuleFor(x => x.Sdnn)
            .NotNull()
            .GreaterThan(0);

        RuleFor(x => x.Si)
            .NotNull()
            .GreaterThan(0);

        RuleFor(x => x.Tp)
            .NotNull()
            .GreaterThan(0);

        RuleFor(x => x.Load)
            .GreaterThanOrEqualTo(0)
            .When(x => x.Load != null);
    }
}