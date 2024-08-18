using System.Runtime.Serialization;

namespace ArtBiathlon.Domain.Exceptions.StatisticalAnalysis.FactorAnalysis;

public class CumulativeVarianceExplainedNullReferenceException : NullReferenceException
{
    public CumulativeVarianceExplainedNullReferenceException() : base(
        "Cumulative variance explained is undefined. It is necessary to conduct factor analysis")
    {
    }

    protected CumulativeVarianceExplainedNullReferenceException(SerializationInfo info, StreamingContext context) :
        base(info, context)
    {
    }

    public CumulativeVarianceExplainedNullReferenceException(string? message) : base(message)
    {
    }

    public CumulativeVarianceExplainedNullReferenceException(string? message, Exception? innerException) : base(message,
        innerException)
    {
    }
}