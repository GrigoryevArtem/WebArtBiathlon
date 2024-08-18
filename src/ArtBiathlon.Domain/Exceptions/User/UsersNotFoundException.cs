using System.Runtime.Serialization;

namespace ArtBiathlon.Domain.Exceptions.User;

public class UsersNotFoundException : NotFoundException
{
    public UsersNotFoundException() : base("Users not found")
    {
    }

    protected UsersNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public UsersNotFoundException(string? message) : base(message)
    {
    }

    public UsersNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}