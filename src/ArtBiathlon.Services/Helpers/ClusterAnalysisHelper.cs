using Accord.MachineLearning;
using Accord.Math.Distances;
using ArtBiathlon.Domain.Models.StatisticalAnalysis;
using ArtBiathlon.Domain.Models.StatisticalAnalysis.ClusterAnalysis;

namespace ArtBiathlon.Services.Helpers;

public class ClusterAnalysisHelper
{
    public List<int>? Labels { get; private set; }
    private KMeansClusterCollection? Clusters { get; set; }

    public static ElbowMethodDto ElbowMethod(double[][] data, double tolerance, int maxIterations)
    {
        const int oneClusterCount = 1;
        var maxClusterCount = data[0].Length;
        var сlusters = Enumerable.Range(oneClusterCount, maxClusterCount).ToList();
        var wcss = new List<double>();

        foreach (var kmeans in сlusters.Select(currentClusters => new KMeans(currentClusters)
                 {
                     Distance = new SquareEuclidean(),
                     Tolerance = tolerance,
                     MaxIterations = maxIterations
                 }))
        {
            kmeans.Learn(data);
            wcss.Add(kmeans.Error);
        }

        var points = сlusters.Zip(wcss, (x, y) => new PointDto(x, y)).ToArray();
        return new ElbowMethodDto(points);
    }

    public Task<KMeans> KMeansFit(double[][] data, int klusterCount)
    {
        return Task.Run(() =>
        {
            var kmeans = new KMeans(klusterCount);
            Clusters = kmeans.Learn(data);
            Labels = Clusters.Decide(data).ToList();
            return kmeans;
        });
    }
}