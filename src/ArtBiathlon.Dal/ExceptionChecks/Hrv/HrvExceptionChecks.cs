using System.Data;
using ArtBiathlon.Domain.Exceptions.Hrv;
using Dapper;

namespace ArtBiathlon.Dal.ExceptionChecks.Hrv;

internal static class HrvExceptionChecks
{
    public static async Task ThrowIfHrvAlreadyExist(long biathleteId, DateTimeOffset createAt, IDbConnection connection)
    {
        var isHrvExists = await IsHrvExistsAsync(biathleteId, createAt, connection);

        if (isHrvExists)
        {
            throw new HrvIndicatorsAlreadyExistsForThisDateException();
        }
    }

    public static async Task ThrowIfHrvNotFoundAsync(long id, IDbConnection connection)
    {
        var isHrvExists = await IsHrvExistsAsync(id, connection);

        if (!isHrvExists)
        {
            throw new HrvIndicatorsNotFoundException();
        }
    }

    private static async Task<bool> IsHrvExistsAsync(
        long biathleteId, DateTimeOffset createdAt, IDbConnection connection)
    {
        const string sqlQuery = @$"
            SELECT EXISTS(
                SELECT 1
                FROM hrv_indicator
                WHERE biathlete_id = @BiathleteId 
                AND created_at = @CreatedAt)";

        var sqlParams = new
        {
            BiathleteId = biathleteId,
            CreatedAt = createdAt
        };
        
        var exists = await connection.QuerySingleAsync<bool>(
            sqlQuery,
            sqlParams);

        return exists;
    }

    private static async Task<bool> IsHrvExistsAsync(long id, IDbConnection connection)
    {
        const string sqlQuery = @$"
            SELECT EXISTS(
                SELECT 1
                FROM hrv_indicator
                WHERE id = @Id)";

        var sqlParams = new
        {
            Id = id
        };
        
        var exists = await connection.QuerySingleAsync<bool>(
            sqlQuery,
            sqlParams);

        return exists;
    }
}