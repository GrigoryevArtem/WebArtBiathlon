using ArtBiathlon.Domain.Enums;

namespace ArtBiathlon.Domain.Models.TrainingSchedule;

public record TrainingsScheduleDto(
    byte TrainingId,
    DateTimeOffset StartDate,
    DateTimeOffset EndDate,
    TimeOfDay DayTime,
    int Duration,
    byte TrainingCampId);