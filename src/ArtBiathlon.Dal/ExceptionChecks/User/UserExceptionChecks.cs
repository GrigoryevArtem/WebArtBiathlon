using System.Data;
using ArtBiathlon.Domain.Exceptions.User;
using Dapper;

namespace ArtBiathlon.Dal.ExceptionChecks.User;

internal static class UserExceptionChecks
{
    public static async Task ThrowIfUserNameAlreadyExistsAsync(string userName, IDbConnection connection)
    {
        var exists = await IsUserExistsByUserNameAsync(userName, connection);

        if (exists)
        {
            throw new UserNameAlreadyExistsException();
        }
    }

    public static async Task ThrowIfUserNotFoundAsync(long userId, IDbConnection connection)
    {
        var exists = await IsUserExistsAsync(userId, connection);

        if (!exists)
        {
            throw new UserNotFoundException();
        }
    }

    public static async Task ThrowIfUserNotFoundByUserNameAsync(string userName, IDbConnection connection)
    {
        var exists = await IsUserExistsByUserNameAsync(userName, connection);

        if (!exists)
        {
            throw new UserNotFoundException();
        }
    }

    private static async Task<bool> IsUserExistsAsync(long userId, IDbConnection connection)
    {
        const string sqlQuery = @"
            SELECT EXISTS(
                SELECT 1
                FROM user_credential
                WHERE id = @UserId
                )";

        var sqlParams = new
        {
            UserId = userId
        };

        var exists = await connection.QuerySingleAsync<bool>(
            sqlQuery,
            sqlParams);

        return exists;
    }

    private static async Task<bool> IsUserExistsByUserNameAsync(
        string login,
        IDbConnection connection)
    {
        const string sqlQuery = @"
            SELECT EXISTS(
                SELECT 1
                FROM user_credential
                WHERE LOWER(login) = LOWER(@Login)
                )";

        var sqlParams = new
        {
            Login = login
        };

        var exists = await connection.QuerySingleAsync<bool>(
            sqlQuery,
            sqlParams);

        return exists;
    }

}