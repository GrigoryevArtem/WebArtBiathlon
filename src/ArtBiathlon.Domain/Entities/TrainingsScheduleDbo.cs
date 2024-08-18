using ArtBiathlon.Domain.Enums;
using ArtBiathlon.Domain.Models;
using ArtBiathlon.Domain.Models.TrainingSchedule;

namespace ArtBiathlon.Domain.Entities;

public record TrainingsScheduleDbo
{
    public TrainingsScheduleDbo()
    {
    }

    public TrainingsScheduleDbo(
        long id,
        byte trainingId,
        DateTimeOffset startDate,
        DateTimeOffset endDate,
        TimeOfDay dayTime,
        int duration,
        byte trainingCampId)
    {
        Id = id;
        TrainingId = trainingId;
        StartDate = startDate;
        EndDate = endDate;
        DayTime = dayTime;
        Duration = duration;
        TrainingCampId = trainingCampId;
    }

    public long Id { get; init; }
    public byte TrainingId { get; init; }
    public DateTimeOffset StartDate { get; init; }
    public DateTimeOffset EndDate { get; init; }
    public TimeOfDay DayTime { get; init; }
    public int Duration { get; init; }
    public byte TrainingCampId { get; init; }

    public ModelDtoWithId<TrainingsScheduleDto> ToModelWithId()
    {
        var trainingsScheduleModel = new TrainingsScheduleDto(
            TrainingId,
            StartDate,
            EndDate,
            DayTime,
            Duration,
            TrainingCampId);

        return new ModelDtoWithId<TrainingsScheduleDto>(
            Id,
            trainingsScheduleModel);
    }
}