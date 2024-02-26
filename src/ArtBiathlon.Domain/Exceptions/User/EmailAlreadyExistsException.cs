using System.Runtime.Serialization;

namespace ArtBiathlon.Domain.Exceptions.User;

public class EmailAlreadyExistsException : Exception
{
    public EmailAlreadyExistsException() : base("Email already exists")
    {
    }

    protected EmailAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public EmailAlreadyExistsException(string? message) : base(message)
    {
    }

    public EmailAlreadyExistsException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}