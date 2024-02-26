using ArtBiathlon.Dal.ExceptionChecks.TrainingType;
using ArtBiathlon.Domain.Entities;
using ArtBiathlon.Domain.Interfaces.Dal;
using ArtBiathlon.Domain.Models;
using ArtBiathlon.Dal.Settings;
using ArtBiathlon.Domain.Exceptions.TrainingType;
using ArtBiathlon.Domain.Interfaces.Dal.TrianingType;
using ArtBiathlon.Domain.Models.TrainingType;
using Dapper;
using Microsoft.Extensions.Options;

namespace ArtBiathlon.Dal.Repositories;

internal class TrainingTypeRepository : DbRepository, ITrainingTypeRepository
{
    public TrainingTypeRepository(IOptions<DalOptions> dalSettings) : base(dalSettings.Value)
    {
    }

    public async Task<byte> CreateTrainingTypeAsync(TrainingTypeDto trainingTypeDto, CancellationToken token)
    {
        await using var connection = await GetAndOpenConnection(token);

        await TrainingTypeExceptionChecks.ThrowIfTrainingExistsAsync(trainingTypeDto.TypeName, connection);
        
        const string sqlQuery = @$"
        INSERT INTO training_type (type_name)
        VALUES (@{nameof(TrainingTypeDto.TypeName)})
        RETURNING id";

        return await connection.QueryFirstAsync<byte>(sqlQuery, trainingTypeDto);
    }
    
    public async Task<ModelDtoWithId<TrainingTypeDto>> GetTrainingTypeByIdAsync(byte id, CancellationToken token)
    {
        await using var connection = await GetAndOpenConnection(token);
        
        await TrainingTypeExceptionChecks.ThrowIfTrainingTypeNotFoundAsync(id, connection);
        
        const string sqlQuery = @$"
        SELECT * FROM training_type
            WHERE id = @Id";

        var sqlParams = new
        {
            Id = id
        };

        var response = await connection.QueryFirstAsync<TrainingTypeDbo>(sqlQuery, sqlParams);

        return response.ToModelWithId();
    }

    public async Task<ModelDtoWithId<TrainingTypeDto>[]> GetTrainingTypesAsync(CancellationToken token)
    {
        await using var connection = await GetAndOpenConnection(token);
        
        const string sqlQuery = "SELECT * FROM training_type";

        var trainingTypes = await connection.QueryAsync<TrainingTypeDbo>(sqlQuery);

        if (trainingTypes is null)
        {
            throw new TrainingTypesNotFoundException();
        }

        return trainingTypes
            .Select(x => x.ToModelWithId())
            .ToArray();
    }

    public async Task DeleteTrainingTypeAsync(byte id, CancellationToken token)
    {
        await using var connection = await GetAndOpenConnection(token);

        await TrainingTypeExceptionChecks.ThrowIfTrainingTypeNotFoundAsync(id, connection);
        
        const string sqlQuery = $@"
        DELETE
        FROM training_type
        WHERE id = @Id";

        var sqlParams = new
        {
            Id = id
        };

        await connection.ExecuteAsync(sqlQuery, sqlParams);
    }

    public async Task UpdateTrainingType(byte id, TrainingTypeDto trainingTypeDto, CancellationToken token)
    {
        await using var connection = await GetAndOpenConnection(token);

        await TrainingTypeExceptionChecks.ThrowIfTrainingTypeNotFoundAsync(id, connection);

        const string sqlQuery = $@"
        UPDATE training_type
        SET type_name = @TypeName
        WHERE id = @Id";

        var sqlParams = new
        {
            Id = id,
            trainingTypeDto.TypeName
        };
        
        await connection.QueryAsync(sqlQuery, sqlParams);
    }
}