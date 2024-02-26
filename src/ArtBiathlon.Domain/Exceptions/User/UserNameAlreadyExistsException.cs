using System.Runtime.Serialization;

namespace ArtBiathlon.Domain.Exceptions.User;

public class UserNameAlreadyExistsException : Exception
{
    public UserNameAlreadyExistsException() : base("Username already exists")
    {
    }

    protected UserNameAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public UserNameAlreadyExistsException(string? message) : base(message)
    {
    }

    public UserNameAlreadyExistsException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}