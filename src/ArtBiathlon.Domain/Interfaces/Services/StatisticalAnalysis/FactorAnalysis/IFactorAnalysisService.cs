using ArtBiathlon.Domain.Enums;
using ArtBiathlon.Domain.Models.Hrv;
using ArtBiathlon.Domain.Models.StatisticalAnalysis;
using ArtBiathlon.Domain.Models.StatisticalAnalysis.FactorAnalysis;

namespace ArtBiathlon.Domain.Interfaces.Services.StatisticalAnalysis.FactorAnalysis;

public interface IFactorAnalysisService
{
    public Task<MatrixDto> NormalizationDataByMinMaxScaler(HrvDto[] hrvIndicators, CancellationToken token);
    public Task InitializationFit(MatrixDto matrix, CancellationToken token);
    public Task<double> GetBartlettTestValue(MatrixDto matrix, CancellationToken token);
    public int GetKaiserCriterionValue();
    public EigenValuesDto[] GetEigenValues();
    public MatrixDto GetFactorTransformationMatrix();
    public FactorLoadingDto GetFactorLoadingMatrix();
    public double[] GetCumulativeVarianceExplained();
    public Task FactorAnalysisFit(MatrixDto matrix, int numOfFactors, CancellationToken token);
    public Dictionary<HrvIndicator, List<ComponentPredominantFactorValuePercentage>> GetRotatedLoadingToMultiplePie();
}