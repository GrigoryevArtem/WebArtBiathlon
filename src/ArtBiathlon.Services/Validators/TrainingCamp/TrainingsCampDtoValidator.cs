using ArtBiathlon.Domain.Models.TrainingsCamp;
using FluentValidation;

namespace ArtBiathlon.Services.Validators.TrainingCamp;

public class TrainingsCampDtoValidator : AbstractValidator<TrainingsCampDto>
{
    public TrainingsCampDtoValidator()
    {
        RuleFor(x => x.CampStart)
            .NotNull()
            .LessThan(x => x.CampEnd);
        
        RuleFor(x => x.CampEnd)
            .NotNull()
            .GreaterThan(x => x.CampStart);
    }
}