using ArtBiathlon.Domain.Enums;

namespace ArtBiathlon.Domain.Models.TrainingSchedule;

public record TrainingsScheduleDisplayModel
{
    public TrainingsScheduleDisplayModel()
    {
    }

    public TrainingsScheduleDisplayModel(
        long id,
        string trainingName,
        string trainingType,
        DateTimeOffset startDate,
        DateTimeOffset endDate,
        TimeOfDay dayTime,
        int duration,
        byte trainingCampId)
    {
        Id = id;
        TrainingName = trainingName;
        TrainingType = trainingType;
        StartDate = startDate;
        EndDate = endDate;
        DayTime = dayTime;
        Duration = duration;
        TrainingCampId = trainingCampId;
    }

    public long Id { get; init; }
    public string TrainingName { get; init; }
    public string TrainingType { get; init; }
    public DateTimeOffset StartDate { get; init; }
    public DateTimeOffset EndDate { get; init; }
    public TimeOfDay DayTime { get; init; }
    public int Duration { get; init; }
    public byte TrainingCampId { get; init; }

    public ModelDtoWithId<TrainingsScheduleDisplayDto> ToModelWithId()
    {
        var trainingsScheduleDisplayModel = new TrainingsScheduleDisplayDto(
            TrainingName,
            TrainingType,
            StartDate,
            EndDate,
            DayTime,
            Duration,
            TrainingCampId);

        return new ModelDtoWithId<TrainingsScheduleDisplayDto>(
            Id,
            trainingsScheduleDisplayModel);
    }
}