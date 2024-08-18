using ArtBiathlon.Domain.Models.TrainingsCamp;

namespace ArtBiathlon.Api.Requests.V1.TrainingCamp;

public record UpdateTrainingCampRequest(
    long Id,
    TrainingsCampDto TrainingsModel);