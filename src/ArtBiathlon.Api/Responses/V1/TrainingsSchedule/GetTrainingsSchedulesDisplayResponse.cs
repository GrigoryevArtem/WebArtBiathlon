using ArtBiathlon.Domain.Models;
using ArtBiathlon.Domain.Models.TrainingSchedule;

namespace ArtBiathlon.Api.Responses.V1.TrainingsSchedule;

public record GetTrainingsSchedulesDisplayResponse(
    ModelDtoWithId<TrainingsScheduleDisplayDto>[] TrainingsScheduleModel);