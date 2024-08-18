using ArtBiathlon.Dal.ExceptionChecks.TrainingCamp;
using ArtBiathlon.Dal.Settings;
using ArtBiathlon.Domain.Entities;
using ArtBiathlon.Domain.Exceptions.TrainingsCamp;
using ArtBiathlon.Domain.Interfaces.Dal.TrainingCamp;
using ArtBiathlon.Domain.Models;
using ArtBiathlon.Domain.Models.TrainingsCamp;
using Dapper;
using Microsoft.Extensions.Options;

namespace ArtBiathlon.Dal.Repositories;

internal class TrainingCampRepository : DbRepository, ITrainingCampRepository
{
    public TrainingCampRepository(IOptions<DalOptions> dalSettings) : base(dalSettings.Value)
    {
    }

    public async Task<long> CreateTrainingCampAsync(TrainingsCampDto trainingCampDto, CancellationToken token)
    {
        await using var connection = await GetAndOpenConnection(token);

        await TrainingCampExceptionChecks.ThrowIfTrainingCampExistsAsync(trainingCampDto.CampStart,
            trainingCampDto.CampEnd, connection);

        const string sqlQuery = @$"
        INSERT INTO training_camp (camp_start, camp_end)
        VALUES ( @{nameof(TrainingsCampDto.CampStart)},
                @{nameof(TrainingsCampDto.CampEnd)})
        RETURNING id";

        return await connection.QueryFirstAsync<long>(sqlQuery, trainingCampDto);
    }

    public async Task<ModelDtoWithId<TrainingsCampDto>> GetTrainingCampByIdAsync(long id, CancellationToken token)
    {
        await using var connection = await GetAndOpenConnection(token);

        await TrainingCampExceptionChecks.ThrowIfTrainingCampNotExistsAsync(id, connection);

        const string sqlQuery = @"
        SELECT * FROM training_camp
            WHERE id = @Id";

        var sqlParams = new
        {
            Id = id
        };

        var response = await connection.QueryFirstAsync<TrainingCampDbo>(sqlQuery, sqlParams);

        return response.ToModelWithId();
    }

    public async Task<ModelDtoWithId<TrainingsCampDto>[]> GetTrainingCampsAsync(CancellationToken token)
    {
        await using var connection = await GetAndOpenConnection(token);

        const string sqlQuery = "SELECT * FROM training_camp";

        var trainingCamps = await connection.QueryAsync<TrainingCampDbo>(sqlQuery);

        if (trainingCamps is null) throw new TrainingsCampsNotFoundException();

        return trainingCamps
            .Select(x => x.ToModelWithId())
            .ToArray();
    }

    public async Task DeleteTrainingCampAsync(long id, CancellationToken token)
    {
        await using var connection = await GetAndOpenConnection(token);

        await TrainingCampExceptionChecks.ThrowIfTrainingCampNotExistsAsync(id, connection);

        const string sqlQuery = @"
        DELETE
        FROM training_camp
        WHERE id = @Id";

        var sqlParams = new
        {
            Id = id
        };

        await connection.ExecuteAsync(sqlQuery, sqlParams);
    }

    public async Task UpdateTrainingCampAsync(long id, TrainingsCampDto trainingsCampDto, CancellationToken token)
    {
        await using var connection = await GetAndOpenConnection(token);

        await TrainingCampExceptionChecks.ThrowIfTrainingCampNotExistsAsync(id, connection);

        const string sqlQuery = @"
        UPDATE training_camp
        SET camp_start = @CampStart,
            camp_end = @CampEnd
        WHERE id = @Id";

        var sqlParams = new
        {
            Id = id,
            trainingsCampDto.CampStart,
            trainingsCampDto.CampEnd
        };

        await connection.QueryAsync(sqlQuery, sqlParams);
    }
}