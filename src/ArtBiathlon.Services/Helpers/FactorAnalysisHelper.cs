using Extreme.Mathematics;
using Extreme.Statistics.Multivariate;
using Extreme.Statistics.Tests;

namespace ArtBiathlon.Services.Helpers;

public class FactorAnalysisHelper
{
    public FactorAnalysisHelper()
    {
    }

    public FactorAnalysisHelper(IEnumerable<double[]> data, int numberOfFactors)
    {
        var factorAnalysis = InitialFit(data, numberOfFactors);
        EigenValues = factorAnalysis.Eigenvalues;
    }

    public Vector<double>? EigenValues { get; private set; }
    public Matrix<double>? RotatedLoadingsMatrix { get; private set; }
    public Vector<double>? VarianceExplained { get; private set; }
    public Vector<double>? CumulativeVarianceExplained { get; private set; }
    public Matrix<double>? FactorTransformationMatrix { get; private set; }

    private static FactorAnalysis InitialFit(IEnumerable<double[]> data, int numberOfFactors)
    {
        var matrix = Matrix.Create(data);

        var factorAnalysis = new FactorAnalysis(matrix)
        {
            NumberOfFactors = numberOfFactors
        };
        factorAnalysis.Fit();

        return factorAnalysis;
    }

    public void FactorAnalysis(IEnumerable<double[]> data, int numberOfFactors)
    {
        var matrix = Matrix.Create(data);
        var factorAnalysis = new FactorAnalysis(matrix)
        {
            RotationMethod = FactorRotationMethod.Varimax,
            ExtractionMethod = FactorExtractionMethod.PrincipalComponents,
            FactorScoreMethod = FactorScoreMethod.Bartlett,
            NumberOfFactors = numberOfFactors
        };
        factorAnalysis.Fit();

        RotatedLoadingsMatrix = factorAnalysis.RotatedLoadingsMatrix;
        VarianceExplained = factorAnalysis.VarianceExplained;
        CumulativeVarianceExplained = factorAnalysis.CumulativeVarianceExplained;
        FactorTransformationMatrix = factorAnalysis.FactorTransformationMatrix;
    }

    public static double GetBartlettTestValue(double[][] data)
    {
        var dataVector = StatisticalAnalysisHelper.ToVectorArray(data);
        var bartlettTest = new BartlettTest(dataVector);

        return bartlettTest.PValue;
    }

    public static int GetKaiserCriterionValue(IEnumerable<double> eigenValues)
    {
        return eigenValues.Count(eigenvalue => eigenvalue > 1.0);
    }
}