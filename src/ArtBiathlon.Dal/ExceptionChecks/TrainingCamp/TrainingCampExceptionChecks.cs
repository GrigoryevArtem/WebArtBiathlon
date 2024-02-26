using System.Data;
using ArtBiathlon.Domain.Exceptions.TrainingsCamp;
using Dapper;

namespace ArtBiathlon.Dal.ExceptionChecks.TrainingCamp;

internal static class TrainingCampExceptionChecks
{
    public static async Task ThrowIfTrainingCampExistsAsync(long id, IDbConnection connection)
    {
        var isTrainingsCampExists = await IsTrainingCampExistsAsync(id, connection);

        if (isTrainingsCampExists)
        {
            throw new TrainingsCampNotFoundException();
        }
    }

    public static async Task ThrowIfTrainingCampNotExistsAsync(long id, IDbConnection connection)
    {
        var isTrainingsCampExists = await IsTrainingCampExistsAsync(id, connection);

        if (!isTrainingsCampExists)
        {
            throw new TrainingsCampNotFoundException();
        }
    }

    private static async Task<bool> IsTrainingCampExistsAsync(long id, IDbConnection connection)
    {
        const string sqlQuery = @$"
            SELECT EXISTS(
                SELECT 1
                FROM training_camp
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