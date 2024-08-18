using ArtBiathlon.Domain.Models;
using ArtBiathlon.Domain.Models.TrainingsCamp;
using FluentValidation;

namespace ArtBiathlon.Services.Validators.TrainingCamp;

public class TrainingsCampWithIdDtoValidator : AbstractValidator<ModelDtoWithId<TrainingsCampDto>>
{
    public TrainingsCampWithIdDtoValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.Model)
            .SetValidator(new TrainingsCampDtoValidator());
    }
}