using ArtBiathlon.Dal.ExceptionChecks.Training;
using ArtBiathlon.Dal.Settings;
using ArtBiathlon.Domain.Entities;
using ArtBiathlon.Domain.Exceptions.Training;
using ArtBiathlon.Domain.Interfaces.Dal.Training;
using ArtBiathlon.Domain.Models;
using ArtBiathlon.Domain.Models.Training;
using Dapper;
using Microsoft.Extensions.Options;

namespace ArtBiathlon.Dal.Repositories;

internal class TrainingRepository : DbRepository, ITrainingRepository
{
    public TrainingRepository(IOptions<DalOptions> dalSettings) : base(dalSettings.Value)
    {
    }

    public async Task<long> CreateTrainingAsync(TrainingDto trainingDto, CancellationToken token)
    {
        await using var connection = await GetAndOpenConnection(token);

        await TrainingExceptionChecks.ThrowIfTrainingExistsAsync(trainingDto.TrainingName, connection);

        const string sqlQuery = @$"
        INSERT INTO training (training_name, training_type_id)
        VALUES (@{nameof(TrainingDto.TrainingName)},
                @{nameof(TrainingDto.TrainingTypeId)})
        RETURNING id";

        return await connection.QueryFirstAsync<long>(sqlQuery, trainingDto);
    }

    public async Task<ModelDtoWithId<TrainingDto>> GetTrainingByIdAsync(long id, CancellationToken token)
    {
        await using var connection = await GetAndOpenConnection(token);

        await TrainingExceptionChecks.ThrowIfTrainingNotFoundAsync(id, connection);

        const string sqlQuery = @$"
        SELECT * FROM training
            WHERE id = @{nameof(id)}";

        var response = await connection.QueryFirstAsync<TrainingDbo>(sqlQuery, id);

        return response.ToModelWithId();
    }

    public async Task<ModelDtoWithId<TrainingDto>[]> GetTrainingsAsync(CancellationToken token)
    {
        await using var connection = await GetAndOpenConnection(token);

        const string sqlQuery = "SELECT * FROM training";

        var training = await connection.QueryAsync<TrainingDbo>(sqlQuery);

        if (training is null) throw new TrainingsNotFoundException();
        return training
            .Select(x => x.ToModelWithId())
            .ToArray();
    }

    public async Task<ModelDtoWithId<TrainingDisplayDto>[]> GetTrainingsDisplayAsync(CancellationToken token)
    {
        await using var connection = await GetAndOpenConnection(token);

        const string sqlQuery = @"
            SELECT training.id,
                   training.training_name,
                   training_type.type_name
                   FROM training
                LEFT JOIN training_type
                    ON training.training_type_id = training_type.id";

        var training = await connection.QueryAsync<TrainingDisplayModel>(sqlQuery);

        if (training is null) throw new TrainingsNotFoundException();
        return training
            .Select(x => x.ToModelWithId())
            .ToArray();
    }

    public async Task DeleteTrainingAsync(long id, CancellationToken token)
    {
        await using var connection = await GetAndOpenConnection(token);

        await TrainingExceptionChecks.ThrowIfTrainingNotFoundAsync(id, connection);

        const string sqlQuery = @"
        DELETE
        FROM training
        WHERE id = @Id";

        var sqlParams = new
        {
            Id = id
        };

        await connection.QueryAsync(sqlQuery, sqlParams);
    }

    public async Task UpdateTrainingAsync(long id, TrainingDto trainingDto, CancellationToken token)
    {
        await using var connection = await GetAndOpenConnection(token);

        await TrainingExceptionChecks.ThrowIfTrainingNotFoundAsync(id, connection);

        const string sqlQuery = @"
        UPDATE training
        SET training_name = @TrainingName,
            training_type_id = @TrainingTypeId
        WHERE id = @Id";

        var sqlParams = new
        {
            Id = id,
            trainingDto.TrainingName,
            trainingDto.TrainingTypeId
        };

        await connection.QueryAsync(sqlQuery, sqlParams);
    }
}