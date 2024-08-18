using ArtBiathlon.Domain.Models;
using ArtBiathlon.Domain.Models.Training;
using FluentValidation;

namespace ArtBiathlon.Services.Validators.Training;

public class TrainingWithIdDtoValidator : AbstractValidator<ModelDtoWithId<TrainingDto>>
{
    public TrainingWithIdDtoValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.Model)
            .SetValidator(new TrainingDtoValidator());
    }
}