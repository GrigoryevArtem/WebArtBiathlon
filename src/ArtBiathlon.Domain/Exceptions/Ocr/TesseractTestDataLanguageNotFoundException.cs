using System.Runtime.Serialization;

namespace ArtBiathlon.Domain.Exceptions.Ocr;

public class TesseractTestDataLanguageNotFoundException : NotFoundException
{
    public TesseractTestDataLanguageNotFoundException() : base("It was not possible to determine the language for" +
                                                      " training the neural network for image analysis")
    {
    }

    protected TesseractTestDataLanguageNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public TesseractTestDataLanguageNotFoundException(string? message) : base(message)
    {
    }

    public TesseractTestDataLanguageNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}