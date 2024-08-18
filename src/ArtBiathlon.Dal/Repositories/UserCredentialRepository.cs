using ArtBiathlon.Dal.ExceptionChecks.User;
using ArtBiathlon.Dal.Settings;
using ArtBiathlon.Domain.Entities;
using ArtBiathlon.Domain.Exceptions.User;
using ArtBiathlon.Domain.Interfaces.Dal.User;
using ArtBiathlon.Domain.Models;
using ArtBiathlon.Domain.Models.User.UserCredential;
using Dapper;
using Microsoft.Extensions.Options;

namespace ArtBiathlon.Dal.Repositories;

internal class UserCredentialRepository : DbRepository, IUserCredentialRepository
{
    public UserCredentialRepository(IOptions<DalOptions> dalSettings) : base(dalSettings.Value)
    {
    }

    public async Task<ModelDtoWithId<UserDto>> GetUserByLoginAsync(string login, CancellationToken token)
    {
        await using var connection = await GetAndOpenConnection(token);

        await UserExceptionChecks.ThrowIfUserNotFoundByUserNameAsync(login, connection);

        const string sqlQuery = @"
        SELECT * FROM user_credential
            WHERE LOWER(login) = LOWER(@Login)";

        var sqlParams = new
        {
            Login = login
        };

        var response = await connection.QueryFirstAsync<UserDbo>(sqlQuery, sqlParams);

        return response.ToModelWithId();
    }

    public async Task UpdateUserAsync(long id, UserDto user, CancellationToken token)
    {
        await using var connection = await GetAndOpenConnection(token);

        await UserExceptionChecks.ThrowIfUserNotFoundAsync(id, connection);

        const string sqlQuery = @"
        UPDATE user_credential
        SET login = @Login,
            password = @Password
        WHERE user_info_id = @Id";

        var sqlParams = new
        {
            Id = id,
            user.Login,
            user.PasswordHash
        };

        await connection.QueryAsync(sqlQuery, sqlParams);
    }

    public async Task<long> CreateUserAsync(UserDto userDto, CancellationToken token)
    {
        await using var connection = await GetAndOpenConnection(token);

        await UserExceptionChecks.ThrowIfUserNameAlreadyExistsAsync(userDto.Login, connection);

        const string sqlQuery = @$"
        INSERT INTO user_credential (login, password)
        VALUES (@{nameof(UserDto.Login)},
                @{nameof(UserDto.PasswordHash)})
        RETURNING user_info_id";

        return await connection.QueryFirstAsync<long>(sqlQuery, userDto);
    }


    public async Task<ModelDtoWithId<UserDto>> GetUserByIdAsync(long id, CancellationToken token)
    {
        await using var connection = await GetAndOpenConnection(token);

        await UserExceptionChecks.ThrowIfUserNotFoundAsync(id, connection);

        const string sqlQuery = @"
        SELECT * FROM user_credential
            WHERE user_info_id = @Id";

        var sqlParams = new
        {
            Id = id
        };

        var response = await connection.QueryFirstAsync<UserDbo>(sqlQuery, sqlParams);

        return response.ToModelWithId();
    }

    public async Task<ModelDtoWithId<UserDto>[]> GetUsersAsync(CancellationToken token)
    {
        await using var connection = await GetAndOpenConnection(token);

        const string sqlQuery = "SELECT * FROM user_credential";

        var users = await connection.QueryAsync<UserDbo>(sqlQuery);

        if (users is null) throw new UsersNotFoundException();
        return users
            .Select(x => x.ToModelWithId())
            .ToArray();
    }

    public async Task DeleteUserAsync(long id, CancellationToken token)
    {
        await using var connection = await GetAndOpenConnection(token);

        await UserExceptionChecks.ThrowIfUserNotFoundAsync(id, connection);

        const string sqlQuery = @"
        DELETE
        FROM user_credential
        WHERE user_info_id = @Id";

        var sqlParams = new
        {
            Id = id
        };

        await connection.ExecuteAsync(sqlQuery, sqlParams);
    }
}