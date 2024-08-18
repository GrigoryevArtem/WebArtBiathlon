using ArtBiathlon.Domain.Enums;

namespace ArtBiathlon.Domain.Models.TrainingSchedule;

public record TrainingsScheduleDisplayDto(
    string TrainingName,
    string TrainingType,
    DateTimeOffset StartDate,
    DateTimeOffset EndDate,
    TimeOfDay DayTime,
    int Duration,
    byte TrainingCampId);