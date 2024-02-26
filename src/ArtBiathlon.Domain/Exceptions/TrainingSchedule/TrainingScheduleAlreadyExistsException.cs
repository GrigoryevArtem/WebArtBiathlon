using System.Runtime.Serialization;

namespace ArtBiathlon.Domain.Exceptions.TrainingSchedule;

public class TrainingScheduleAlreadyExistsException : Exception
{
    public TrainingScheduleAlreadyExistsException() : base("Training schedule already exists")
    {
    }

    protected TrainingScheduleAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public TrainingScheduleAlreadyExistsException(string? message) : base(message)
    {
    }

    public TrainingScheduleAlreadyExistsException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}