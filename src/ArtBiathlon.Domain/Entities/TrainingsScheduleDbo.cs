using ArtBiathlon.Domain.Enums;
using ArtBiathlon.Domain.Models;
using ArtBiathlon.Domain.Models.TrainingSchedule;

namespace ArtBiathlon.Domain.Entities;

public record TrainingsScheduleDbo
{
    public long Id { get; init; }
    public byte TrainingId { get; init; }
    public DateTimeOffset StartDate { get; init; }
    public TimeOfDay DayTime { get; init; }
    public int Duration { get; init; }
    public byte CampId { get; init; }

    public TrainingsScheduleDbo()
    {
    }

    public TrainingsScheduleDbo(
        long id,
        byte trainingId,
        DateTimeOffset startDate,
        TimeOfDay dayTime,
        int duration,
        byte campId)
    {
        Id = id;
        TrainingId = trainingId;
        StartDate = startDate;
        DayTime = dayTime;
        Duration = duration;
        CampId = campId;
    }

    public ModelDtoWithId<TrainingsScheduleDto> ToModelWithId()
    {
        var trainingsScheduleModel = new TrainingsScheduleDto(
            TrainingId,
            StartDate,
            DayTime,
            Duration,
            CampId);

        return new ModelDtoWithId<TrainingsScheduleDto>(
            Id,
            trainingsScheduleModel);
    }
}