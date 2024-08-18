using ArtBiathlon.Domain.Exceptions.StatisticalAnalysis;
using ArtBiathlon.Domain.Models.Hrv;
using ArtBiathlon.Domain.Models.StatisticalAnalysis;
using Extreme.Mathematics;

namespace ArtBiathlon.Services.Helpers;

public static class StatisticalAnalysisHelper
{
    private static IEnumerable<double> MinMaxScaler(IReadOnlyList<double> data)
    {
        var min = data.Min();
        var max = data.Max();
        var normalizedData = new double[data.Count];

        const double tolerance = 0.0001;
        if (Math.Abs(min - max) < tolerance) throw new SampleInsufficientNumberElementsException();

        for (var i = 0; i < data.Count; i++) normalizedData[i] = Math.Round((data[i] - min) / (max - min), 2);

        return normalizedData;
    }

    public static HrvAverageDto GetHrvAverage(HrvDto[] hrvIndicators)
    {
        return new HrvAverageDto
        {
            Readiness = hrvIndicators.Average(x => x.Readiness),
            Heart = hrvIndicators.Average(x => x.Heart),
            Rmssd = hrvIndicators.Average(x => x.Rmssd),
            Rr = hrvIndicators.Average(x => x.Rr),
            Sdnn = hrvIndicators.Average(x => x.Sdnn),
            Sd = hrvIndicators.Average(x => x.Sd),
            Tp = hrvIndicators.Average(x => x.Tp),
            Hf = hrvIndicators.Average(x => x.Hf),
            Lf = hrvIndicators.Average(x => x.Lf),
            Si = hrvIndicators.Average(x => x.Si),
            Load = hrvIndicators.Average(x => x.Load ?? 0.0)
        };
    }

    public static double[][] NormalizationDataByMinMaxScaler(HrvDto[] hrvIndicators)
    {
        double[][] normalizedData =
        {
            MinMaxScaler(hrvIndicators.Select(x => x.Readiness).ToList()).ToArray(),
            MinMaxScaler(hrvIndicators.Select(x => (double)x.Heart).ToList()).ToArray(),
            MinMaxScaler(hrvIndicators.Select(x => (double)x.Rmssd).ToList()).ToArray(),
            MinMaxScaler(hrvIndicators.Select(x => x.Rr).ToList()).ToArray(),
            MinMaxScaler(hrvIndicators.Select(x => x.Sdnn).ToList()).ToArray(),
            MinMaxScaler(hrvIndicators.Select(x => x.Sd).ToList()).ToArray(),
            MinMaxScaler(hrvIndicators.Select(x => x.Tp).ToList()).ToArray(),
            MinMaxScaler(hrvIndicators.Select(x => x.Hf).ToList()).ToArray(),
            MinMaxScaler(hrvIndicators.Select(x => x.Lf).ToList()).ToArray(),
            MinMaxScaler(hrvIndicators.Select(x => x.Si).ToList()).ToArray(),
            MinMaxScaler(hrvIndicators.Select(x => x.Load ?? 0.0).ToList()).ToArray()
        };

        return normalizedData;
    }

    public static double[][] HrvDtoArrayToDimensionalArray(HrvDto[] hrvIndicators)
    {
        return hrvIndicators.Select(dto => new[]
            {
                dto.Readiness,
                dto.Heart,
                dto.Rmssd,
                dto.Rr,
                dto.Sdnn,
                dto.Sd,
                dto.Tp,
                dto.Hf,
                dto.Lf,
                dto.Si,
                dto.Load ?? 0.0
            })
            .ToArray();
    }

    public static Vector<double>[] ToVectorArray(double[][] matrixArray)
    {
        return matrixArray.Select(row => (Vector<double>)row.ToVector()).ToArray();
    }

    public static double[][] ConvertMatrixToArray2D(Matrix<double> matrix)
    {
        var rows = matrix.RowCount;
        var columns = matrix.ColumnCount;
        var result = new double[rows][];

        for (var i = 0; i < rows; i++)
        {
            result[i] = new double[columns];
            for (var j = 0; j < columns; j++) result[i][j] = matrix[i, j];
        }

        return result;
    }

    public static IReadOnlyCollection<double> GetColumn(double[][] matrix, int columnIndex)
    {
        var rows = matrix.GetLength(0);
        var column = new double[rows];

        for (var i = 0; i < rows; i++)
            column[i] = matrix[i][columnIndex];

        return column;
    }
}