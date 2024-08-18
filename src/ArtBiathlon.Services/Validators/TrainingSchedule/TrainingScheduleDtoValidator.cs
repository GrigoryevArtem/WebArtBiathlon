using ArtBiathlon.Domain.Models.TrainingSchedule;
using FluentValidation;

namespace ArtBiathlon.Services.Validators.TrainingSchedule;

public class TrainingScheduleDtoValidator : AbstractValidator<TrainingsScheduleDto>
{
    public TrainingScheduleDtoValidator()
    {
        RuleFor(x => x.Duration)
            .NotNull()
            .Must((trainingsScheduleDto, _) => IsValidDuration(trainingsScheduleDto))
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.TrainingCampId)
            .NotNull()
            .GreaterThan((byte)0);

        RuleFor(x => x.DayTime)
            .NotNull();

        RuleFor(x => x.TrainingId)
            .NotNull()
            .GreaterThan((byte)0);

        RuleFor(x => x.StartDate)
            .NotNull()
            .GreaterThanOrEqualTo(DateTimeOffset.Now.AddYears(-5))
            .LessThanOrEqualTo(x => x.EndDate);

        RuleFor(x => x.EndDate)
            .NotNull()
            .LessThanOrEqualTo(DateTimeOffset.Now.AddYears(5))
            .GreaterThan(x => x.StartDate);
    }

    private bool IsValidDuration(TrainingsScheduleDto trainingsScheduleDto)
    {
        if (trainingsScheduleDto.StartDate == default || trainingsScheduleDto.EndDate == default) return false;

        var durationInMinutes = (trainingsScheduleDto.EndDate - trainingsScheduleDto.StartDate).TotalMinutes;
        return trainingsScheduleDto.Duration == (int)durationInMinutes;
    }
}