using ArtBiathlon.Domain.Models.TrainingSchedule;

namespace ArtBiathlon.Api.Requests.V1.TrainingsSchedule;

public record UpdateTrainingsScheduleRequest(
    long Id,
    TrainingsScheduleDto TrainingsSchedule);