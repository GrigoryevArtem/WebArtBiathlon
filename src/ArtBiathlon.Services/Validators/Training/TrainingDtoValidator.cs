using ArtBiathlon.Domain.Models.Training;
using FluentValidation;

namespace ArtBiathlon.Services.Validators.Training;

public class TrainingDtoValidator : AbstractValidator<TrainingDto>
{
    public TrainingDtoValidator()
    {
        RuleFor(x => x.TrainingName.Length)
            .NotNull()
            .LessThanOrEqualTo(100);
        
        RuleFor(x => x.TrainingTypeId)
            .NotNull()
            .GreaterThan((byte)0);
    }
}