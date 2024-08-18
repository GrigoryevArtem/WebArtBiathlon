using Accord.Math;
using ArtBiathlon.Domain.Enums;
using ArtBiathlon.Domain.Exceptions.StatisticalAnalysis.FactorAnalysis;
using ArtBiathlon.Domain.Interfaces.Services.StatisticalAnalysis.FactorAnalysis;
using ArtBiathlon.Domain.Models.Hrv;
using ArtBiathlon.Domain.Models.StatisticalAnalysis;
using ArtBiathlon.Domain.Models.StatisticalAnalysis.FactorAnalysis;
using ArtBiathlon.Services.Helpers;
using Extreme;
using Extreme.Mathematics;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ArtBiathlon.Services.Services.StatisticalAnalysis.FactorAnalysis;

public class FactorAnalysisService : IFactorAnalysisService
{
    private readonly IServiceProvider _provider;
    private FactorAnalysisHelper _factorAnalysisHelper = new();

    public FactorAnalysisService(IConfiguration configuration, IServiceProvider provider)
    {
        _provider = provider.CreateScope().ServiceProvider;
        License.Verify(configuration.GetValue<string>("ExtremeLicense"));
    }

    public async Task<MatrixDto> NormalizationDataByMinMaxScaler(HrvDto[] hrvIndicators, CancellationToken token)
    {
        var validator = _provider.GetService<IValidator<HrvDto>>();
        foreach (var hrvIndicator in hrvIndicators) await validator.ValidateAndThrowAsync(hrvIndicator, token);

        var matrix = StatisticalAnalysisHelper.NormalizationDataByMinMaxScaler(hrvIndicators);
        matrix = matrix.Transpose();
        return new MatrixDto(matrix);
    }

    public async Task InitializationFit(MatrixDto matrix, CancellationToken token)
    {
        var validator = _provider.GetService<IValidator<MatrixDto>>();
        await validator.ValidateAndThrowAsync(matrix, token);
        var matrixData = matrix.Data;
        const int firstRowIndex = 0;
        var nunOfFactors = matrixData[firstRowIndex].Length;
        _factorAnalysisHelper = new FactorAnalysisHelper(matrixData, nunOfFactors);
    }

    public async Task<double> GetBartlettTestValue(MatrixDto matrix, CancellationToken token)
    {
        var validator = _provider.GetService<IValidator<MatrixDto>>();
        await validator.ValidateAndThrowAsync(matrix, token);
        return FactorAnalysisHelper.GetBartlettTestValue(matrix.Data);
    }

    public int GetKaiserCriterionValue()
    {
        var eigenValues = _factorAnalysisHelper.EigenValues;
        if (eigenValues is null) throw new EigenValuesNullReferenceException();
        return FactorAnalysisHelper.GetKaiserCriterionValue(eigenValues);
    }

    public EigenValuesDto[] GetEigenValues()
    {
        if (_factorAnalysisHelper.EigenValues is null) throw new EigenValuesNullReferenceException();
        var eigenValuesArray = _factorAnalysisHelper.EigenValues.ToArray();

        var i = 0;
        var eigenValuesEnumerable = eigenValuesArray
            .Select(x => new EigenValuesDto(++i, Math.Round(x, 2)))
            .ToArray();

        return eigenValuesEnumerable;
    }

    public MatrixDto GetFactorTransformationMatrix()
    {
        if (_factorAnalysisHelper.FactorTransformationMatrix is null)
            throw new FactorTransformationMatrixNullReferenceException();

        var matrix =
            StatisticalAnalysisHelper.ConvertMatrixToArray2D(_factorAnalysisHelper.FactorTransformationMatrix);

        return new MatrixDto(matrix);
    }

    public FactorLoadingDto GetFactorLoadingMatrix()
    {
        if (_factorAnalysisHelper.RotatedLoadingsMatrix is null)
            throw new RotatedLoadingsMatrixNullReferenceException();

        if (_factorAnalysisHelper.VarianceExplained is null) throw new VarianceExplainedNullReferenceException();

        var rotatedLoadingMatrix =
            StatisticalAnalysisHelper.ConvertMatrixToArray2D(_factorAnalysisHelper.RotatedLoadingsMatrix);

        var varianceExplained = _factorAnalysisHelper.VarianceExplained.ToArray();

        return new FactorLoadingDto(rotatedLoadingMatrix, varianceExplained);
    }

    public double[] GetCumulativeVarianceExplained()
    {
        if (_factorAnalysisHelper.CumulativeVarianceExplained is null)
            throw new CumulativeVarianceExplainedNullReferenceException();

        return _factorAnalysisHelper.CumulativeVarianceExplained.ToArray();
    }

    public async Task FactorAnalysisFit(MatrixDto matrix, int numOfFactors, CancellationToken token)
    {
        var validator = _provider.GetService<IValidator<MatrixDto>>();
        await validator.ValidateAndThrowAsync(matrix, token);
        _factorAnalysisHelper.FactorAnalysis(matrix.Data, numOfFactors);
    }

    public Dictionary<HrvIndicator, List<ComponentPredominantFactorValuePercentageDto>> GetRotatedLoadingToMultiplePie()
    {
        var rotatedLoadingsMatrix = _factorAnalysisHelper.RotatedLoadingsMatrix;

        if (rotatedLoadingsMatrix is null)
            throw new RotatedLoadingsMatrixNullReferenceException();

        var componentPredominantFactorValuePercentageByHrvIndicator =
            new Dictionary<HrvIndicator, List<ComponentPredominantFactorValuePercentageDto>>();

        const double determiningPredominantValueLimit = 0.7;

        var columnsSum =
            GetColumnComponentPredominantFactorMatrixSumDictionary(rotatedLoadingsMatrix,
                determiningPredominantValueLimit);

        for (var i = 0; i < rotatedLoadingsMatrix.RowCount; ++i)
        {
            var currentIndicator = (HrvIndicator)i;
            for (var j = 0; j < rotatedLoadingsMatrix.ColumnCount; ++j)
            {
                if (!(Math.Abs(rotatedLoadingsMatrix[i, j]) >= determiningPredominantValueLimit)) continue;
                var componentPercentValue = Math.Abs(rotatedLoadingsMatrix[i, j]) / columnsSum[j] * 100;
                if (componentPredominantFactorValuePercentageByHrvIndicator.ContainsKey(currentIndicator))
                    componentPredominantFactorValuePercentageByHrvIndicator[currentIndicator].Add(
                        new ComponentPredominantFactorValuePercentageDto(
                            (byte)(j + 1),
                            componentPercentValue));
                else
                    componentPredominantFactorValuePercentageByHrvIndicator.Add(currentIndicator,
                        new List<ComponentPredominantFactorValuePercentageDto>
                            { new((byte)(j + 1), componentPercentValue) });
            }
        }

        return componentPredominantFactorValuePercentageByHrvIndicator;
    }

    private static Dictionary<int, double> GetColumnComponentPredominantFactorMatrixSumDictionary(
        Matrix<double> rotatedLoadingsMatrix,
        double determiningPredominantValueLimit)
    {
        var columnsSum = new Dictionary<int, double>();

        for (var j = 0; j < rotatedLoadingsMatrix.ColumnCount; ++j)
        {
            var columnValues = rotatedLoadingsMatrix.GetColumn(j);
            columnsSum[j] =
                GetColumnComponentPredominantFactorMatrixSum(columnValues, determiningPredominantValueLimit);
        }

        return columnsSum;
    }

    private static double GetColumnComponentPredominantFactorMatrixSum(Vector<double> columnValues,
        double determiningPredominantValueLimit)
    {
        return columnValues
            .Select(Math.Abs)
            .Where(x => x >= determiningPredominantValueLimit)
            .Sum();
    }
}