using System.Runtime.Serialization;

namespace ArtBiathlon.Domain.Exceptions.TrainingSchedule;

public class TrainingScheduleNotFoundException : Exception
{
    public TrainingScheduleNotFoundException() : base("Training schedule not found")
    {
    }

    protected TrainingScheduleNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public TrainingScheduleNotFoundException(string? message) : base(message)
    {
    }

    public TrainingScheduleNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}