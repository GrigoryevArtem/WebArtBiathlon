using System.Runtime.Serialization;

namespace ArtBiathlon.Domain.Exceptions.StatisticalAnalysis.FactorAnalysis;

public class VarianceExplainedNullReferenceException : NullReferenceException
{
    public VarianceExplainedNullReferenceException() : base(
        "Variance explained is undefined. It is necessary to conduct factor analysis")
    {
    }

    protected VarianceExplainedNullReferenceException(SerializationInfo info, StreamingContext context) :
        base(info, context)
    {
    }

    public VarianceExplainedNullReferenceException(string? message) : base(message)
    {
    }

    public VarianceExplainedNullReferenceException(string? message, Exception? innerException) : base(message,
        innerException)
    {
    }
}