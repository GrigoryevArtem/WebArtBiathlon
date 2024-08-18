using System.Runtime.Serialization;

namespace ArtBiathlon.Domain.Exceptions.StatisticalAnalysis;

public class SampleInsufficientNumberElementsException : IncorrectParametersException
{
    public SampleInsufficientNumberElementsException() : base(
        "Insufficient number of elements in the sample")
    {
    }

    protected SampleInsufficientNumberElementsException(SerializationInfo info, StreamingContext context) : base(info,
        context)
    {
    }

    public SampleInsufficientNumberElementsException(string? message) : base(message)
    {
    }

    public SampleInsufficientNumberElementsException(string? message, Exception? innerException) : base(message,
        innerException)
    {
    }
}