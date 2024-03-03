using ArtBiathlon.Domain.Models.TrainingSchedule;
using FluentValidation;

namespace ArtBiathlon.Services.Validators.TrainingSchedule;

public class TrainingScheduleDtoValidator : AbstractValidator<TrainingsScheduleDto>
{
    public TrainingScheduleDtoValidator()
    {
        RuleFor(x => x.Duration)
            .NotNull()
            .GreaterThan(0);

        RuleFor(x => x.CampId)
            .NotNull()
            .GreaterThan((byte)0);

        RuleFor(x => x.DayTime)
            .NotNull();

        RuleFor(x => x.TrainingId)
            .NotNull()
            .GreaterThan((byte)0);

        RuleFor(x => x.StartDate)
            .NotNull()
            .GreaterThanOrEqualTo(DateTimeOffset.Now.AddYears(-5));
    }
}