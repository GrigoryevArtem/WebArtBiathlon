using ArtBiathlon.Dal.ExceptionChecks.Hrv;
using ArtBiathlon.Dal.Settings;
using ArtBiathlon.Domain.Entities;
using ArtBiathlon.Domain.Exceptions.Hrv;
using ArtBiathlon.Domain.Interfaces.Dal.Hrv;
using ArtBiathlon.Domain.Models;
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

        await HrvExceptionChecks.ThrowIfHrvAlreadyExist(hrvDto.UserInfoId, hrvDto.CreatedAt, connection);

        const string query = @$"
        INSERT INTO hrv_indicator (created_at, user_info_id, readiness,
                                   heart,     rmssd,        rr,
                                   sdnn,      sd,           tp,
                                   hf,        lf,           si, 
                                   load)
        VALUES (@{nameof(HrvDto.CreatedAt)}, @{nameof(HrvDto.UserInfoId)}, @{nameof(HrvDto.Readiness)},
                @{nameof(HrvDto.Heart)},    @{nameof(HrvDto.Rmssd)},       @{nameof(HrvDto.Rr)},
                @{nameof(HrvDto.Sdnn)},     @{nameof(HrvDto.Sd)},          @{nameof(HrvDto.Tp)},
                @{nameof(HrvDto.Hf)},       @{nameof(HrvDto.Lf)},          @{nameof(HrvDto.Si)},
                @{nameof(HrvDto.Load)})
        RETURNING id";

        return await connection.QueryFirstAsync<long>(query, hrvDto);
    }

    public async Task<ModelDtoWithId<HrvDto>> GetHrvByIdAsync(long id, CancellationToken token)
    {
        await using var connection = await GetAndOpenConnection(token);

        await HrvExceptionChecks.ThrowIfHrvNotFoundAsync(id, connection);

        const string sqlQuery = @"
        SELECT * FROM hrv_indicator
            WHERE id = @Id";

        var sqlParams = new
        {
            Id = id
        };

        var hrvIndicator = await connection.QuerySingleAsync<HrvDbo>(sqlQuery, sqlParams);
        return hrvIndicator.ToModelWithId();
    }

    public async Task<ModelDtoWithId<HrvDto>[]> GetHrvsAsync(CancellationToken token)
    {
        await using var connection = await GetAndOpenConnection(token);

        const string sqlQuery = "SELECT * FROM hrv_indicator";

        var hrvIndicators = await connection.QueryAsync<HrvDbo>(sqlQuery);

        if (hrvIndicators is null) throw new HrvIndicatorsNotFoundInThisTimeException();

        return hrvIndicators
            .Select(x => x.ToModelWithId())
            .ToArray();
    }

    public async Task<ModelDtoWithId<HrvDto>[]> GetHrvsAsync(int[] userInfoIds, int[] campIds, CancellationToken token)
    {
        await using var connection = await GetAndOpenConnection(token);

        const string sqlQuery = @"
           SELECT hrv_indicator.id,
                  created_at,
                  user_info_id,
                  readiness,
                  heart,
                  rmssd,
                  rr,
                  sdnn,
                  sd,
                  tp,
                  hf,
                  lf,
                  si,
                  load
                FROM hrv_indicator
                LEFT JOIN trainings_schedule ON hrv_indicator.created_at = trainings_schedule.start_date :: date
                WHERE user_info_id = ANY(@UserInfoIds)
         AND trainings_schedule.training_camp_id = ANY(@CampIds)";

        var sqlParams = new
        {
            UserInfoIds = userInfoIds,
            CampIds = campIds
        };

        var hrvIndicators = await connection.QueryAsync<HrvDbo>(sqlQuery, sqlParams);

        if (hrvIndicators is null) throw new HrvIndicatorsNotFoundInThisTimeException();

        return hrvIndicators
            .Select(x => x.ToModelWithId())
            .ToArray();
    }

    public async Task<ModelDtoWithId<HrvDto>[]> GetHrvsWeekendAsync(int[] userInfoIds, int[] campIds,
        CancellationToken token)
    {
        await using var connection = await GetAndOpenConnection(token);

        const string sqlQuery = @"
           SELECT hrv_indicator.id,
                  created_at,
                  user_info_id,
                  readiness,
                  heart,
                  rmssd,
                  rr,
                  sdnn,
                  sd,
                  tp,
                  hf,
                  lf,
                  si,
                  load
                FROM hrv_indicator
                LEFT JOIN trainings_schedule ON hrv_indicator.created_at = trainings_schedule.start_date :: date
                WHERE user_info_id = ANY(@UserInfoIds)
                AND load is null
                AND trainings_schedule.training_camp_id = ANY(@CampIds)";

        var sqlParams = new
        {
            UserInfoIds = userInfoIds,
            CampIds = campIds
        };


        var hrvIndicators = await connection.QueryAsync<HrvDbo>(sqlQuery, sqlParams);

        if (hrvIndicators is null) throw new HrvIndicatorsNotFoundInThisTimeException();

        return hrvIndicators
            .Select(x => x.ToModelWithId())
            .ToArray();
    }

    public async Task<ModelDtoWithId<HrvDto>[]> GetHrvsWeekdaysAsync(int[] userInfoIds, int[] campIds,
        CancellationToken token)
    {
        await using var connection = await GetAndOpenConnection(token);

        const string sqlQuery = @"
           SELECT hrv_indicator.id,
                  created_at,
                  user_info_id,
                  readiness,
                  heart,
                  rmssd,
                  rr,
                  sdnn,
                  sd,
                  tp,
                  hf,
                  lf,
                  si,
                  load
                FROM hrv_indicator
                LEFT JOIN trainings_schedule ON hrv_indicator.created_at = trainings_schedule.start_date :: date
                WHERE user_info_id = ANY(@UserInfoIds)
                AND load is not null
                AND trainings_schedule.training_camp_id = ANY(@CampIds)";

        var sqlParams = new
        {
            UserInfoIds = userInfoIds,
            CampIds = campIds
        };

        var hrvIndicators = await connection.QueryAsync<HrvDbo>(sqlQuery, sqlParams);

        if (hrvIndicators is null) throw new HrvIndicatorsNotFoundInThisTimeException();

        return hrvIndicators
            .Select(x => x.ToModelWithId())
            .ToArray();
    }

    public async Task<ModelDtoWithId<HrvDto>[]>
        GetHrvsAsync(int userInfoIds, int trainingCampIds, CancellationToken token)
    {
        await using var connection = await GetAndOpenConnection(token);

        const string sqlQuery = @"
           SELECT hrv_indicator.id,
                  created_at,
                  user_info_id,
                  readiness,
                  heart,
                  rmssd,
                  rr,
                  sdnn,
                  sd,
                  tp,
                  hf,
                  lf,
                  si,
                  load
                FROM hrv_indicator
                LEFT JOIN trainings_schedule ON hrv_indicator.created_at = trainings_schedule.start_date :: date
                WHERE user_info_id = @UserInfoIds
        AND trainings_schedule.camp_id = @TrainingCampId";

        var sqlParams = new
        {
            UserInfoIds = userInfoIds,
            TrainingCampId = trainingCampIds
        };

        var hrvIndicators = await connection.QueryAsync<HrvDbo>(sqlQuery, sqlParams);

        if (hrvIndicators is null) throw new HrvIndicatorsNotFoundInThisTimeException();

        return hrvIndicators
            .Select(x => x.ToModelWithId())
            .ToArray();
    }

    public async Task DeleteHrvAsync(long id, CancellationToken token)
    {
        await using var connection = await GetAndOpenConnection(token);

        await HrvExceptionChecks.ThrowIfHrvNotFoundAsync(id, connection);

        const string sqlQuery = @"
        DELETE
        FROM hrv_indicator
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

        const string sqlQuery = @"
        UPDATE hrv_indicator
        SET created_at = @CreatedAt,
            user_info_id = @UserInfoId,
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
            hrvDto.UserInfoId,
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