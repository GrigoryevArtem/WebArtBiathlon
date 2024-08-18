using ArtBiathlon.Domain.Models;
using ArtBiathlon.Domain.Models.TrainingSchedule;
using FluentValidation;

namespace ArtBiathlon.Services.Validators.TrainingSchedule;

public class TrainingScheduleWithIdDtoValidator : AbstractValidator<ModelDtoWithId<TrainingsScheduleDto>>
{
    public TrainingScheduleWithIdDtoValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.Model)
            .SetValidator(new TrainingScheduleDtoValidator());
    }
}