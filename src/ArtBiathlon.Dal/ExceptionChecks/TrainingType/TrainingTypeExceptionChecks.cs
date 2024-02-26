using System.Data;
using ArtBiathlon.Domain.Exceptions.TrainingType;
using Dapper;

namespace ArtBiathlon.Dal.ExceptionChecks.TrainingType;

internal static class TrainingTypeExceptionChecks
{
    public static async Task ThrowIfTrainingExistsAsync(string typeName, IDbConnection connection)
    {
        var isTrainingTypeExists = await IsTrainingTypeExistsAsync(typeName, connection);

        if (isTrainingTypeExists)
        {
            throw new TrainingTypeAlreadyExistsException();
        }
    }

    public static async Task ThrowIfTrainingTypeNotFoundAsync(string typeName, IDbConnection connection)
    {
        var isTrainingTypeExists = await IsTrainingTypeExistsAsync(typeName, connection);

        if (!isTrainingTypeExists)
        {
            throw new TrainingTypeNotFoundException();
        }
    }

    public static async Task ThrowIfTrainingTypeNotFoundAsync(byte id, IDbConnection connection)
    {
        var isTrainingTypeExists = await IsTrainingTypeExistsAsync(id, connection);

        if (!isTrainingTypeExists)
        {
            throw new TrainingTypeNotFoundException();
        }
    }
    
    public static async Task ThrowIfTrainingExistsAsync(byte id, IDbConnection connection)
    {
        var isTrainingTypeExists = await IsTrainingTypeExistsAsync(id, connection);

        if (isTrainingTypeExists)
        {
            throw new TrainingTypeAlreadyExistsException();
        }
    }

    private static async Task<bool> IsTrainingTypeExistsAsync(string typeName, IDbConnection connection)
    {
        const string sqlQuery = @$"
            SELECT EXISTS(
                SELECT 1
                FROM training_type
                WHERE type_name = @TypeName)";

        var sqlParams = new
        {
            TypeName = typeName
        };
        
        var exists = await connection.QuerySingleAsync<bool>(
            sqlQuery,
            sqlParams);

        return exists;
    }

    private static async Task<bool> IsTrainingTypeExistsAsync(byte id, IDbConnection connection)
    {
        const string sqlQuery = @$"
            SELECT EXISTS(
                SELECT 1
                FROM training_type
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