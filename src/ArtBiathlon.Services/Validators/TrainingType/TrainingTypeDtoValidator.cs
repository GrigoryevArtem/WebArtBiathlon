using ArtBiathlon.Domain.Models.TrainingType;
using FluentValidation;

namespace ArtBiathlon.Services.Validators.TrainingType;

public class TrainingTypeDtoValidator : AbstractValidator<TrainingTypeDto>
{
    public TrainingTypeDtoValidator()
    {
        RuleFor(x => x.TypeName.Length)
            .NotNull()
            .LessThanOrEqualTo(100);
    }
}