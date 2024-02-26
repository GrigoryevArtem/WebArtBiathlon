using System.Data;
using ArtBiathlon.Domain.Exceptions.Training;
using Dapper;

namespace ArtBiathlon.Dal.ExceptionChecks.Training;

internal static class TrainingExceptionChecks
{
    public static async Task ThrowIfTrainingExistsAsync(string trainingName, IDbConnection connection)
    {
        var isTrainingTypeExists = await IsTrainingExistsAsync(trainingName, connection);

        if (isTrainingTypeExists)
        {
            throw new TrainingAlreadyExistsException();
        }
    }

    public static async Task ThrowIfTrainingExistsAsync(long id, IDbConnection connection)
    {
        var isTrainingTypeExists = await IsTrainingExistsAsync(id, connection);

        if (isTrainingTypeExists)
        {
            throw new TrainingAlreadyExistsException();
        }
    }
    
    public static async Task ThrowIfTrainingNotFoundAsync(string trainingName, IDbConnection connection)
    {
        var isTrainingTypeExists = await IsTrainingExistsAsync(trainingName, connection);

        if (!isTrainingTypeExists)
        {
            throw new TrainingNotFoundException();
        }
    }

    public static async Task ThrowIfTrainingNotFoundAsync(long id, IDbConnection connection)
    {
        var isTrainingTypeExists = await IsTrainingExistsAsync(id, connection);

        if (!isTrainingTypeExists)
        {
            throw new TrainingNotFoundException();
        }
    }

    private static async Task<bool> IsTrainingExistsAsync(string trainingName, IDbConnection connection)
    {
        const string sqlQuery = @$"
            SELECT EXISTS(
                SELECT 1
                FROM training
                WHERE training_name = @TrainingName)";

        var sqlParams = new
        {
            TrainingName = trainingName
        };
        
        var exists = await connection.QuerySingleAsync<bool>(
            sqlQuery,
            sqlParams);

        return exists;
    }

    private static async Task<bool> IsTrainingExistsAsync(long id, IDbConnection connection)
    {
        const string sqlQuery = @$"
            SELECT EXISTS(
                SELECT 1
                FROM training
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