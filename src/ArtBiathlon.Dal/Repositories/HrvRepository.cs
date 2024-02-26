using ArtBiathlon.Dal.ExceptionChecks.Hrv;
using ArtBiathlon.Domain.Interfaces.Dal;
using ArtBiathlon.Dal.Settings;
using ArtBiathlon.Domain.Exceptions.Hrv;
using ArtBiathlon.Domain.Interfaces.Dal.Hrv;
using ArtBiathlon.Domain.Models.Hrv;
using Dapper;
using Microsoft.Extensions.Options;

namespace ArtBiathlon.Dal.Repositories;

internal class HrvRepository : DbRepository, IHrvRepository
{
    public HrvRepository(IOptions<DalOptions> dalSettings) : base(dalSettings.Value)
    {
    }

    public async Task<long> CreateHrvAsync(HrvDto hrvDto, CancellationToken token)
    {
        await using var connection = await GetAndOpenConnection(token);

        await HrvExceptionChecks.ThrowIfHrvAlreadyExist(hrvDto.BiathleteId, hrvDto.CreatedAt, connection);

        const string query = @$"
        INSERT INTO hrv_indicator (created_at, biathlete_id, readiness
                                   heart,     rmssd,        rr
                                   sdnn,      sd,           tp 
                                   hf,        lf,           si, 
                                   load)
        VALUES (@{nameof(HrvDto.CreatedAt)}, @{nameof(HrvDto.BiathleteId)}, @{nameof(HrvDto.Readiness)},
                @{nameof(HrvDto.Heart)},    @{nameof(HrvDto.Rmssd)},       @{nameof(HrvDto.Rr)},
                @{nameof(HrvDto.Sdnn)},     @{nameof(HrvDto.Sd)},          @{nameof(HrvDto.Tp)},
                @{nameof(HrvDto.Hf)},       @{nameof(HrvDto.Lf)},          @{nameof(HrvDto.Si)},
                @{nameof(HrvDto.Load)})";

        return await connection.ExecuteAsync(query, hrvDto);
    }

    public async Task<HrvDto> GetHrvByIdAsync(long id, CancellationToken token)
    {
        await using var connection = await GetAndOpenConnection(token);

        await HrvExceptionChecks.ThrowIfHrvNotFoundAsync(id, connection);

        const string query = @$"
        SELECT * FROM hrv_indicator
            WHERE id = @Id";

        var sqlParams = new
        {
            Id = id
        };

        return await connection.QuerySingleAsync<HrvDto>(query, sqlParams);
    }

    public async Task<HrvDto[]> GetHrvsAsync(CancellationToken token)
    {
        await using var connection = await GetAndOpenConnection(token);

        const string sqlQuery = "SELECT * FROM hrv_indicator";

        var hrvIndicators = await connection.QueryAsync<HrvDto>(sqlQuery);

        if (hrvIndicators is null)
        {
            throw new HrvIndicatorsNotFoundInThisTime();
        }

        return hrvIndicators
            .ToArray();
    }

    public async Task DeleteHrvAsync(long id, CancellationToken token)
    {
        await using var connection = await GetAndOpenConnection(token);

        await HrvExceptionChecks.ThrowIfHrvNotFoundAsync(id, connection);

        const string sqlQuery = $@"
        DELETE
        FROM hrv
        WHERE id = @Id";

        var sqlParams = new
        {
            Id = id
        };

        await connection.ExecuteAsync(sqlQuery, sqlParams);
    }

    public async Task UpdateHrvAsync(long id, HrvDto hrvDto, CancellationToken token)
    {
        await using var connection = await GetAndOpenConnection(token);

        await HrvExceptionChecks.ThrowIfHrvNotFoundAsync(id, connection);

        const string sqlQuery = $@"
        UPDATE hrv
        SET created_at = @CreatedAt,
            biathlete_id = @BiathleteId,
            heart = @Heart,
            rmssd = @Rmssd,
            hf = @Hf,
            lf = @Lf,
            readiness = @Readiness,
            rr = @Rr,
            sd = @Sd,
            sdnn = @Sdnn,
            si = @Si,
            tp = @Tp,
            load = @Load
        WHERE id = @Id";

        var sqlParams = new
        {
            Id = id,
            hrvDto.BiathleteId,
            hrvDto.CreatedAt,
            hrvDto.Heart,
            hrvDto.Rmssd,
            hrvDto.Hf,
            hrvDto.Lf,
            hrvDto.Readiness,
            hrvDto.Rr,
            hrvDto.Sd,
            hrvDto.Sdnn,
            hrvDto.Si,
            hrvDto.Tp,
            hrvDto.Load
        };

        await connection.QueryAsync(sqlQuery, sqlParams);
    }
}