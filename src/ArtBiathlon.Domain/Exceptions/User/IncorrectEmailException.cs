using System.Runtime.Serialization;

namespace ArtBiathlon.Domain.Exceptions.User;

public class IncorrectEmailException : IncorrectParametersException
{
    public IncorrectEmailException() : base("Incorrect Email")
    {
    }

    protected IncorrectEmailException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public IncorrectEmailException(string? message) : base(message)
    {
    }

    public IncorrectEmailException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}