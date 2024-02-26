using ArtBiathlon.Domain.Enums;

namespace ArtBiathlon.Domain.Models.TrainingSchedule;

public record TrainingsScheduleDto (
    byte TrainingId,
    DateTimeOffset StartDate,
    TimeOfDay DayTime,
    int Duration,
    byte CampId);