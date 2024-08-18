using System.Runtime.Serialization;

namespace ArtBiathlon.Domain.Exceptions.Ocr;

public class FilesNotFoundException : NotFoundException
{
    public FilesNotFoundException() : base("File(-s) not found")
    {
    }

    protected FilesNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public FilesNotFoundException(string? message) : base(message)
    {
    }

    public FilesNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}