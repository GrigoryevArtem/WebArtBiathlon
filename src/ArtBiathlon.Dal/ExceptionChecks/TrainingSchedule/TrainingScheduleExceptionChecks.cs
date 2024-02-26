using System.Data;
using ArtBiathlon.Domain.Exceptions.TrainingSchedule;
using Dapper;

namespace ArtBiathlon.Dal.ExceptionChecks.TrainingSchedule;

internal static class TrainingScheduleExceptionChecks
{
    public static async Task ThrowIfTrainingScheduleExistsAsync(long id, IDbConnection connection)
    {
        var isTrainingsCampExists = await IsTrainingScheduleExistsAsync(id, connection);

        if (isTrainingsCampExists)
        {
            throw new TrainingScheduleAlreadyExistsException();
        }
    }

    public static async Task ThrowIfTrainingScheduleNotExistsAsync(long id, IDbConnection connection)
    {
        var isTrainingsCampExists = await IsTrainingScheduleExistsAsync(id, connection);

        if (!isTrainingsCampExists)
        {
            throw new TrainingScheduleNotFoundException();
        }
    }
    
    public static async Task ThrowIfTrainingScheduleExistsAsync(DateTimeOffset startDate, IDbConnection connection)
    {
        var isTrainingsCampExists = await IsTrainingScheduleExistsAsync(startDate, connection);

        if (isTrainingsCampExists)
        {
            throw new TrainingScheduleAlreadyExistsException();
        }
    }

    public static async Task ThrowIfTrainingScheduleNotExistsAsync(DateTimeOffset startDate, IDbConnection connection)
    {
        var isTrainingsCampExists = await IsTrainingScheduleExistsAsync(startDate, connection);

        if (!isTrainingsCampExists)
        {
            throw new TrainingScheduleNotFoundException();
        }
    }

    //todo: написать метод, который будет проверять при добавлении новой тренировки время запланированное плюс ее продолжительность
    private static async Task<bool> IsTrainingScheduleExistsAsync(DateTimeOffset startDate, IDbConnection connection)
    {
        const string sqlQuery = @$"
            SELECT EXISTS(
                SELECT 1
                FROM trainings_schedule
                WHERE start_date = @StartDate)";

        var sqlParams = new
        {
            StartDate = startDate
        };
        
        var exists = await connection.QuerySingleAsync<bool>(
            sqlQuery,
            sqlParams);

        return exists;
    }
    
    private static async Task<bool> IsTrainingScheduleExistsAsync(long id, IDbConnection connection)
    {
        const string sqlQuery = @$"
            SELECT EXISTS(
                SELECT 1
                FROM trainings_schedule
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