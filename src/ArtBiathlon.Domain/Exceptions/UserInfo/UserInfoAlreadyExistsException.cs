using System.Runtime.Serialization;

namespace ArtBiathlon.Domain.Exceptions.UserInfo;

public class UserInfoAlreadyExistsException : Exception
{
    public UserInfoAlreadyExistsException() : base("This user info already exists")
    {
        
    }

    protected UserInfoAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public UserInfoAlreadyExistsException(string? message) : base(message)
    {
    }

    public UserInfoAlreadyExistsException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}