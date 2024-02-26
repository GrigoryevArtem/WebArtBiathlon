using System.Runtime.Serialization;

namespace ArtBiathlon.Domain.Exceptions.User;

public class IncorrectPasswordException : Exception
{
    public IncorrectPasswordException() : base("Incorrect password")
    {
    }

    protected IncorrectPasswordException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public IncorrectPasswordException(string? message) : base(message)
    {
    }

    public IncorrectPasswordException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}