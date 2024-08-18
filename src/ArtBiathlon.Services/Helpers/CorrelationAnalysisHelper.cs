using ArtBiathlon.Domain.Models.StatisticalAnalysis;
using ArtBiathlon.Domain.Models.StatisticalAnalysis.CorrelationAnalysis;

namespace ArtBiathlon.Services.Helpers;

public static class CorrelationAnalysisHelper
{
    public static double[][] GetPairwiseDependenceCoefficientsMatrix(double[][] values)
    {
        var columnsCount = values[0].Length;

        var correlationMatrix = new double[columnsCount][];

        for (var i = 0; i < columnsCount; ++i)
        {
            correlationMatrix[i] = new double[columnsCount];

            for (var j = 0; j < columnsCount; ++j)
                if (i == j)
                {
                    correlationMatrix[i][j] = 1.0;
                }
                else
                {
                    var valuesX = StatisticalAnalysisHelper.GetColumn(values, i);
                    var valuesY = StatisticalAnalysisHelper.GetColumn(values, j);
                    var correlation = GetLinearPearsonCorrelationCoefficient(valuesX, valuesY);
                    correlationMatrix[i][j] = correlation;
                }
        }

        return correlationMatrix;
    }

    public static DistributionChartDto GetDistributionChart(double[] data)
    {
        var max = data.Max();
        var min = data.Min();

        var tickAmount = Math.Ceiling(1 + 3.32 * Math.Log10(data.Length));
        var step = (max - min) / tickAmount;

        var points = new List<PointDto>();

        for (var i = 1; i <= tickAmount; i++)
        {
            var lower = data[0] + (i - 1) * step;
            var top = data[0] + i * step;
            var mid = (top + lower) / 2;
            mid = Math.Round(mid, 2);
            var count = 0;

            for (var j = 0; j < data.Length; ++j)
                if (j == 0)
                {
                    if (data[j] >= lower && data[j] < top) count++;
                }
                else
                {
                    if (data[j] > lower && data[j] <= top) count++;
                }

            points.Add(new PointDto(mid, count));
        }

        return new DistributionChartDto(
            points.ToArray(),
            min,
            max,
            tickAmount);
    }

    private static double GetLinearPearsonCorrelationCoefficient(IReadOnlyCollection<double> valuesX,
        IReadOnlyCollection<double> valuesY)
    {
        var covariance = GetCovarianceValue(valuesX, valuesY);
        var varianceX = GetVarianceValue(valuesX);
        var varianceY = GetVarianceValue(valuesY);

        return covariance / Math.Sqrt((varianceX == 0.0 ? 1 : varianceX) * (varianceY == 0.0 ? 1 : varianceY));
    }

    private static double GetAverageValue(IEnumerable<double> values)
    {
        return values.Average();
    }

    private static double GetVarianceValue(IReadOnlyCollection<double> values)
    {
        var average = GetAverageValue(values);
        return values.Select(x => double.Pow(x - average, 2)).Sum();
    }

    private static double GetCovarianceValue(IReadOnlyCollection<double> valuesX, IReadOnlyCollection<double> valuesY)
    {
        var averageX = GetAverageValue(valuesX);
        var averageY = GetAverageValue(valuesY);
        return valuesX.Zip(valuesY, (x, y) => (x - averageX) * (y - averageY)).Sum();
    }
}