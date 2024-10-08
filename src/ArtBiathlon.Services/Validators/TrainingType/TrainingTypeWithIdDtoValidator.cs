using ArtBiathlon.Domain.Models;
using ArtBiathlon.Domain.Models.TrainingType;
using FluentValidation;

namespace ArtBiathlon.Services.Validators.TrainingType;

public class TrainingTypeWithIdDtoValidator : AbstractValidator<ModelDtoWithId<TrainingTypeDto>>
{
    public TrainingTypeWithIdDtoValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.Model)
            .SetValidator(new TrainingTypeDtoValidator());
    }
}