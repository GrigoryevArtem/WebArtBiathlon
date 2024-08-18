using ArtBiathlon.Dal.ExceptionChecks.TrainingSchedule;
using ArtBiathlon.Dal.Settings;
using ArtBiathlon.Domain.Entities;
using ArtBiathlon.Domain.Exceptions.TrainingSchedule;
using ArtBiathlon.Domain.Interfaces.Dal.TrainingsSchedule;
using ArtBiathlon.Domain.Models;
using ArtBiathlon.Domain.Models.TrainingSchedule;
using Dapper;
using Microsoft.Extensions.Options;

namespace ArtBiathlon.Dal.Repositories;

internal class TrainingsScheduleRepository : DbRepository, ITrainingsScheduleRepository
{
    public TrainingsScheduleRepository(IOptions<DalOptions> dalSettings) : base(dalSettings.Value)
    {
    }

    public async Task<long> CreateTrainingsScheduleAsync(TrainingsScheduleDto trainingScheduleDto,
        CancellationToken token)
    {
        await using var connection = await GetAndOpenConnection(token);

        await TrainingScheduleExceptionChecks.ThrowIfTrainingScheduleExistsAsync(trainingScheduleDto.StartDate,
            trainingScheduleDto.EndDate,
            connection);

        const string sqlQuery = @$"
        INSERT INTO trainings_schedule (training_id, start_date, end_date, day_time, duration, training_camp_id)
        VALUES ( @{nameof(TrainingsScheduleDto.TrainingId)},
                 @{nameof(TrainingsScheduleDto.StartDate)},
                 @{nameof(TrainingsScheduleDto.EndDate)},
                 @{nameof(TrainingsScheduleDto.DayTime)},
                 make_interval(mins := @{nameof(TrainingsScheduleDto.Duration)}),
                 @{nameof(TrainingsScheduleDto.TrainingCampId)})
        RETURNING id";

        return await connection.QueryFirstAsync<byte>(sqlQuery, trainingScheduleDto);
    }

    public async Task<ModelDtoWithId<TrainingsScheduleDto>> GetTrainingsScheduleAsync(DateTimeOffset startDate,
        CancellationToken token)
    {
        await using var connection = await GetAndOpenConnection(token);

        await TrainingScheduleExceptionChecks.ThrowIfTrainingScheduleNotExistsAsync(startDate, connection);

        const string sqlQuery = @"
        SELECT 
            id,
            training_id                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        ning_id,
            start_date,
            end_date,
            day_time,
            EXTRACT(epoch FROM duration) / 60::int as duration,
            training_camp_id
        FROM trainings_schedule
            WHERE start_date :: date = @StartDate :: date";


        var sqlParams = new
        {
            StartDate = startDate
        };

        var response = await connection.QueryFirstAsync<TrainingsScheduleDbo>(sqlQuery, sqlParams);

        return response.ToModelWithId();
    }

    public async Task<ModelDtoWithId<TrainingsScheduleDto>> GetTrainingsScheduleAsync(long id,
        CancellationToken token)
    {
        await using var connection = await GetAndOpenConnection(token);

        await TrainingScheduleExceptionChecks.ThrowIfTrainingScheduleNotExistsAsync(id, connection);

        const string sqlQuery = @"
        SELECT 
            id,
            training_id,
            start_date,
            end_date,
            day_time,
            EXTRACT(epoch FROM duration) / 60::int as duration,
            training_camp_id
        FROM trainings_schedule
            WHERE id = @Id";

        var sqlParams = new
        {
            Id = id
        };

        var response = await connection.QueryFirstAsync<TrainingsScheduleDbo>(sqlQuery, sqlParams);

        return response.ToModelWithId();
    }

    public async Task<ModelDtoWithId<TrainingsScheduleDto>[]> GetTrainingsSchedulesAsync(CancellationToken token)
    {
        await using var connection = await GetAndOpenConnection(token);

        const string sqlQuery = @"
        SELECT 
            id,
            training_id,
            start_date,
            end_date,
            day_time,
            EXTRACT(epoch FROM duration) / 60::int as duration,
            training_camp_id
        FROM trainings_schedule";

        var trainingsSchedule = await connection.QueryAsync<TrainingsScheduleDbo>(sqlQuery);

        if (trainingsSchedule is null) throw new TrainingScheduleNotFoundException();

        return trainingsSchedule
            .Select(x => x.ToModelWithId())
            .ToArray();
    }

    public async Task<ModelDtoWithId<TrainingsScheduleDisplayDto>[]> GetTrainingsSchedulesDisplayAsync(
        CancellationToken token)
    {
        await using var connection = await GetAndOpenConnection(token);

        const string sqlQuery = @"
        SELECT 
            ts.id,
            t.training_name,
            tt.type_name as training_type,
            ts.start_date,
            ts.end_date,
            ts.day_time,
            EXTRACT(epoch FROM ts.duration) / 60::int as duration,
            ts.training_camp_id
        FROM trainings_schedule ts
            INNER JOIN training t on t.id = ts.training_id
            INNER JOIN training_type tt on t.training_type_id = tt.id";

        var trainingsSchedule = await connection
            .QueryAsync<TrainingsScheduleDisplayModel>(sqlQuery);

        if (trainingsSchedule is null) throw new TrainingScheduleNotFoundException();

        return trainingsSchedule
            .Select(x => x.ToModelWithId())
            .ToArray();
    }

    public async Task DeleteTrainingsScheduleAsync(long id, CancellationToken token)
    {
        await using var connection = await GetAndOpenConnection(token);

        await TrainingScheduleExceptionChecks.ThrowIfTrainingScheduleNotExistsAsync(id, connection);

        const string sqlQuery = @"
        DELETE
        FROM trainings_schedule
        WHERE id = @Id";

        var sqlParams = new
        {
            Id = id
        };

        await connection.ExecuteAsync(sqlQuery, sqlParams);
    }

    public async Task UpdateTrainingScheduleAsync(long id, TrainingsScheduleDto trainingsScheduleDto,
        CancellationToken token)
    {
        await using var connection = await GetAndOpenConnection(token);

        await TrainingScheduleExceptionChecks.ThrowIfTrainingScheduleNotExistsAsync(id, connection);

        const string sqlQuery = @"
        UPDATE trainings_schedule
        SET training_id = @TrainingId,
            start_date = @StartDate,
            end_date = @EndDate,
            day_time = @DayTime,
            duration = make_interval(mins := @Duration),
            training_camp_id = @TrainingCampId
        WHERE id = @Id";

        var sqlParams = new
        {
            Id = id,
            trainingsScheduleDto.TrainingId,
            trainingsScheduleDto.StartDate,
            trainingsScheduleDto.EndDate,
            trainingsScheduleDto.Duration,
            trainingsScheduleDto.TrainingCampId,
            trainingsScheduleDto.DayTime
        };

        await connection.QueryAsync(sqlQuery, sqlParams);
    }
}