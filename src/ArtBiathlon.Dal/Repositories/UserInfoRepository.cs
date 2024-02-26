using ArtBiathlon.Dal.ExceptionChecks.UserInfo;
using ArtBiathlon.Domain.Entities;
using ArtBiathlon.Domain.Interfaces.Dal;
using ArtBiathlon.Domain.Models;
using ArtBiathlon.Dal.Settings;
using ArtBiathlon.Domain.Exceptions.UserInfo;
using ArtBiathlon.Domain.Interfaces.Dal.UserInfo;
using ArtBiathlon.Domain.Models.User.UserInfo;
using Dapper;
using Microsoft.Extensions.Options;

namespace ArtBiathlon.Dal.Repositories;

internal class UserInfoRepository : DbRepository, IUserInfoRepository
{
    public UserInfoRepository(IOptions<DalOptions> dalSettings) : base(dalSettings.Value)
    {
    }
    
    public async Task<long> CreateUserInfoAsync(UserInfoDto userInfoDto, CancellationToken token)
    {
        const string sqlQuery = @$"
        INSERT INTO user_info(surname, name, middle_name, birth_date, gender, rank, status, email, user_avatar)
        VALUES ( @{nameof(UserInfoDto.Surname)},
                 @{nameof(UserInfoDto.Name)},
                 @{nameof(UserInfoDto.MiddleName)},
                 @{nameof(UserInfoDto.BirthDate)},
                 @{nameof(UserInfoDto.Gender)},
                 @{nameof(UserInfoDto.Rank)},
                 @{nameof(UserInfoDto.Status)},
                 @{nameof(UserInfoDto.Email)},
                 @{nameof(UserInfoDto.UserAvatar)})
        RETURNING id";

        await using var connection = await GetAndOpenConnection(token);

        return await connection.QueryFirstAsync<long>(sqlQuery, userInfoDto);
    }
    
    public async Task UpdateUserInfoAsync(long id, UserInfoDto userInfo, CancellationToken token)
    {
        await using var connection = await GetAndOpenConnection(token);
        
        await UserInfoExceptionChecks.ThrowIfUserNotExistsAsync(id, connection);
        
        const string sqlQuery = 
            @"
            UPDATE user_info 
            SET surname = @Surname, 
                name = @Name, 
                middle_name = @MiddleName, 
                birth_date = @BirthDate,
                gender = @Gender, 
                rank = @Rank, 
                status = @Status, 
                email = @Email,
                user_avatar = @UserAvatar
            WHERE id = @Id
            ";
        
        var sqlParams = new
        {
            Id = id,
            userInfo.Surname,
            userInfo.Name,
            userInfo.MiddleName,
            userInfo.BirthDate,
            userInfo.Gender,
            userInfo.Rank,
            userInfo.Status,
            userInfo.Email,
            userInfo.UserAvatar
        };
        
        await connection.QueryAsync(sqlQuery, sqlParams);
    }
    public async Task<ModelDtoWithId<UserInfoDto>> GetUserInfoByIdAsync(long id, CancellationToken token)
    {
        await using var connection = await GetAndOpenConnection(token);

        await UserInfoExceptionChecks.ThrowIfUserNotExistsAsync(id, connection);
        
        const string sqlQuery = @$"
        SELECT * FROM user_info
            WHERE id = @Id";

        var sqlParams = new
        {
            Id = id
        };
        
        return await connection.QueryFirstAsync<ModelDtoWithId<UserInfoDto>>(sqlQuery, sqlParams);
    }

    public async Task<ModelDtoWithId<UserInfoDto>[]> GetUsersInfoAsync(CancellationToken token)
    {
        await using var connection = await GetAndOpenConnection(token);
        
        const string sqlQuery = "SELECT * FROM user_info";

        var usersInfo = await connection.QueryAsync<UserInfoDbo>(sqlQuery);

        if (usersInfo is null)
        {
            throw new UsersInfoNotFoundException();
        }

        return usersInfo
            .Select(x => x.ToModelWithId())
            .ToArray();
    }

    public async Task DeleteUserInfoAsync(long id, CancellationToken token)
    {
        await using var connection = await GetAndOpenConnection(token);

        await UserInfoExceptionChecks.ThrowIfUserNotExistsAsync(id, connection);

        const string sqlQuery = $@"
        DELETE
        FROM user_info
        WHERE id = @Id";
        
        var sqlParams = new
        {
            Id = id
        };
        
        await connection.ExecuteAsync(sqlQuery, sqlParams);
    }
}