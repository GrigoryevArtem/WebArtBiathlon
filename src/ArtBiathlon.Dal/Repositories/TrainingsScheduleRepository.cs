using ArtBiathlon.Dal.ExceptionChecks.TrainingSchedule;
using ArtBiathlon.Domain.Entities;
using ArtBiathlon.Domain.Interfaces.Dal;
using ArtBiathlon.Domain.Models;
using ArtBiathlon.Dal.Settings;
using ArtBiathlon.Domain.Interfaces.Dal.TrainingsSchedule;
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
            connection);
        
        const string sqlQuery = @$"
        INSERT INTO trainings_schedule (training_id, start_date, day_time, duration, camp_id)
        VALUES ( @{nameof(TrainingsScheduleDto.TrainingId)},
                 @{nameof(TrainingsScheduleDto.StartDate)},
                 @{nameof(TrainingsScheduleDto.DayTime)},
                 make_interval(mins := @{nameof(TrainingsScheduleDto.Duration)}),
                 @{nameof(TrainingsScheduleDto.CampId)})
        RETURNING id";

        return await connection.QueryFirstAsync<byte>(sqlQuery, trainingScheduleDto);
    }

    //todo: добавить получение тренировки по дате и времени 

    public async Task<ModelDtoWithId<TrainingsScheduleDto>> GetTrainingsScheduleByIdAsync(long id,
        CancellationToken token)
    {
        await using var connection = await GetAndOpenConnection(token);

        await TrainingScheduleExceptionChecks.ThrowIfTrainingScheduleNotExistsAsync(id, connection);
        
        const string sqlQuery = @$"
        SELECT 
            id,
            training_id,
            start_date,
            day_time,
            EXTRACT(epoch FROM duration) / 60::int as duration,
            camp_id
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
        
        const string sqlQuery = @$"
        SELECT 
            id,
            training_id,
            start_date,
            day_time,
            EXTRACT(epoch FROM duration) / 60::int as duration,
            camp_id
        FROM trainings_schedule";
        
        var trainingsSchedule = await connection.QueryAsync<TrainingsScheduleDbo>(sqlQuery);

        if (trainingsSchedule is null)
        {
            //todo: заглушка, добавить offset по хорошему бы 
            throw new Exception();
        }

        return trainingsSchedule
            .Select(x => x.ToModelWithId())
            .ToArray();
    }

    public async Task DeleteTrainingsScheduleAsync(long id, CancellationToken token)
    {
        await using var connection = await GetAndOpenConnection(token);

        await TrainingScheduleExceptionChecks.ThrowIfTrainingScheduleNotExistsAsync(id, connection);
        
        const string sqlQuery = $@"
        DELETE
        FROM trainings_schedule
        WHERE id = @Id";

        var sqlParams = new
        {
            Id = id
        };

        await connection.ExecuteAsync(sqlQuery, sqlParams);
    }

    public async Task UpdateTrainingScheduleAsync(long id, TrainingsScheduleDto trainingsScheduleDto, CancellationToken token)
    {
        await using var connection = await GetAndOpenConnection(token);

        await TrainingScheduleExceptionChecks.ThrowIfTrainingScheduleNotExistsAsync(id, connection);

        const string sqlQuery = $@"
        UPDATE trainings_schedule
        SET training_id = @TrainingId,
            start_date = @StartDate,
            day_time = @DayTime,
            duration = @Duration,
            camp_id = @CampId
        WHERE id = @Id";

        var sqlParams = new
        {
            Id = id,
            trainingsScheduleDto.TrainingId,
            trainingsScheduleDto.StartDate,
            trainingsScheduleDto.Duration,
            trainingsScheduleDto.CampId,
            trainingsScheduleDto.DayTime
        };
        
        await connection.QueryAsync(sqlQuery, sqlParams);
    }
}