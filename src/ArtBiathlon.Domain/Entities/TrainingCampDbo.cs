using ArtBiathlon.Domain.Models;
using ArtBiathlon.Domain.Models.TrainingsCamp;

namespace ArtBiathlon.Domain.Entities;

public record TrainingCampDbo
{
    public TrainingCampDbo()
    {
    }

    public TrainingCampDbo(long id, DateTimeOffset campStart, DateTimeOffset campEnd)
    {
        Id = id;
        CampStart = campStart;
        CampEnd = campEnd;
    }

    public long Id { get; init; }
    public DateTimeOffset CampStart { get; init; }
    public DateTimeOffset CampEnd { get; init; }

    public ModelDtoWithId<TrainingsCampDto> ToModelWithId()
    {
        var trainingCampModel = new TrainingsCampDto(
            CampStart,
            CampEnd);

        return new ModelDtoWithId<TrainingsCampDto>(
            Id,
            trainingCampModel);
    }
}