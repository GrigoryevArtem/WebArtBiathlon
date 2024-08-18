namespace ArtBiathlon.Domain.Models.TrainingsCamp;

public record TrainingsCampDto(
    DateTimeOffset CampStart,
    DateTimeOffset CampEnd);