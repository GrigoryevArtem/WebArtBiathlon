using System.Runtime.Serialization;

namespace ArtBiathlon.Domain.Exceptions.TrainingsCamp;

public class TrainingsCampsAlreadyExistsException : AlreadyExistException
{
    public TrainingsCampsAlreadyExistsException() : base("Trainings camps already exists")
    {
    }

    protected TrainingsCampsAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info,
        context)
    {
    }

    public TrainingsCampsAlreadyExistsException(string? message) : base(message)
    {
    }

    public TrainingsCampsAlreadyExistsException(string? message, Exception? innerException) : base(message,
        innerException)
    {
    }
}