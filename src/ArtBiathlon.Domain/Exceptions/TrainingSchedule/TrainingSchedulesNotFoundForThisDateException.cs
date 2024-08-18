using System.Runtime.Serialization;

namespace ArtBiathlon.Domain.Exceptions.TrainingSchedule;

public class TrainingSchedulesNotFoundForThisDateException : NotFoundException
{
    public TrainingSchedulesNotFoundForThisDateException() : base(
        "There are no scheduled training sessions for the selected date")
    {
    }

    protected TrainingSchedulesNotFoundForThisDateException(SerializationInfo info, StreamingContext context) :
        base(info, context)
    {
    }

    public TrainingSchedulesNotFoundForThisDateException(string? message) : base(message)
    {
    }

    public TrainingSchedulesNotFoundForThisDateException(string? message, Exception? innerException) : base(message,
        innerException)
    {
    }
}