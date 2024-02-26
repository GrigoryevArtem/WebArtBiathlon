using ArtBiathlon.Domain.Models.Hrv;
using FluentValidation;
namespace ArtBiathlon.Services.Validators.Hrv;

public class HrvDtoValidator : AbstractValidator<HrvDto>
{
    public HrvDtoValidator()
    {
        RuleFor(x => x.BiathleteId)
            .NotNull()
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.CreatedAt)
            .NotNull()
            .GreaterThanOrEqualTo(DateTimeOffset.Now.AddYears(-5))
            .LessThanOrEqualTo(DateTimeOffset.Now);
        
        RuleFor(x => x.Heart)
            .NotNull()
            .GreaterThanOrEqualTo(0);
        
        RuleFor(x => x.Lf)
            .NotNull()
            .GreaterThanOrEqualTo(0);
        
        RuleFor(x => x.Rmssd)
            .NotNull()
            .GreaterThanOrEqualTo(0);
        
        RuleFor(x => x.Rr)
            .NotNull()
            .GreaterThanOrEqualTo(0);
        
        RuleFor(x => x.Sd)
            .NotNull()
            .GreaterThanOrEqualTo(0);
        
        RuleFor(x => x.Hf)
            .NotNull()
            .GreaterThanOrEqualTo(0);
        
        RuleFor(x => x.Readiness)
            .NotNull()
            .GreaterThanOrEqualTo(0);
        
        RuleFor(x => x.Sdnn)
            .NotNull()
            .GreaterThanOrEqualTo(0);
        
        RuleFor(x => x.Si)
            .NotNull()
            .GreaterThanOrEqualTo(0);
        
        RuleFor(x => x.Tp)
            .NotNull()
            .GreaterThanOrEqualTo(0);
        
        RuleFor(x => x.Load)
            .GreaterThanOrEqualTo(0);
    }
}