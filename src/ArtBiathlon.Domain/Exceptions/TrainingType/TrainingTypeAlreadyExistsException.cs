using System.Runtime.Serialization;

namespace ArtBiathlon.Domain.Exceptions.TrainingType;

public class TrainingTypeAlreadyExistsException : AlreadyExistException
{
    public TrainingTypeAlreadyExistsException() : base("This training type already exists")
    {
    }

    protected TrainingTypeAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public TrainingTypeAlreadyExistsException(string? message) : base(message)
    {
    }

    public TrainingTypeAlreadyExistsException(string? message, Exception? innerException) : base(message,
        innerException)
    {
    }
}