using System.Runtime.Serialization;

namespace ArtBiathlon.Domain.Exceptions.Ocr;

public class TesseractTestDataPathNotFoundException : NotFoundException
{
    public TesseractTestDataPathNotFoundException() : base("It was not possible to determine the path to the training" +
                                                  " data for image analysis by a neural network")
    {
    }

    protected TesseractTestDataPathNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public TesseractTestDataPathNotFoundException(string? message) : base(message)
    {
    }

    public TesseractTestDataPathNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}