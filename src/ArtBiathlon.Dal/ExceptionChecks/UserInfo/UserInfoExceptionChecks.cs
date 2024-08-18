using System.Data;
using ArtBiathlon.Domain.Exceptions.User;
using ArtBiathlon.Domain.Exceptions.UserInfo;
using Dapper;

namespace ArtBiathlon.Dal.ExceptionChecks.UserInfo;

internal static class UserInfoExceptionChecks
{
    public static async Task ThrowIfUserExistsAsync(long id, IDbConnection connection)
    {
        var isUserExists = await IsUserInfoExistsAsync(id, connection);

        if (isUserExists) throw new UserInfoAlreadyExistsException();
    }

    public static async Task ThrowIfUserNotExistsAsync(long id, IDbConnection connection)
    {
        var isUserExists = await IsUserInfoExistsAsync(id, connection);

        if (!isUserExists) throw new UserInfoNotFoundException();
    }

    public static async Task ThrowIfEmailAlreadyExistsAsync(string email, IDbConnection connection)
    {
        var exists = await IsEmailExistsAsync(email, connection);

        if (exists) throw new EmailAlreadyExistsException();
    }

    public static async Task ThrowIfUserNotFoundByEmailAsync(string email, IDbConnection connection)
    {
        var exists = await IsUserExistsByEmailAsync(email, connection);

        if (!exists) throw new UserNotFoundException();
    }

    public static async Task ThrowIfEmailNotFoundAsync(string email, IDbConnection connection)
    {
        var exists = await IsEmailExistsAsync(email, connection);

        if (!exists) throw new EmailNotConfirmedException();
    }


    private static async Task<bool> IsUserInfoExistsAsync(long id, IDbConnection connection)
    {
        const string sqlQuery = @"
            SELECT EXISTS(
                SELECT 1
                FROM user_info
                WHERE id = @Id)";

        var sqlParams = new
        {
            Id = id
        };

        var exists = await connection.QuerySingleAsync<bool>(
            sqlQuery,
            sqlParams);

        return exists;
    }

    private static async Task<bool> IsUserExistsByEmailAsync(
        string email,
        IDbConnection connection)
    {
        const string sqlQuery = @"
            SELECT EXISTS(
                SELECT 1
                FROM user_info
                WHERE LOWER(email) = LOWER(@Email)
                )";

        var sqlParams = new
        {
            Email = email
        };

        var exists = await connection.QuerySingleAsync<bool>(
            sqlQuery,
            sqlParams);

        return exists;
    }

    private static async Task<bool> IsEmailExistsAsync(string email, IDbConnection connection)
    {
        const string sqlQuery = @"
            SELECT EXISTS(
                SELECT 1
                FROM user_info
                WHERE email = @Email
                )";

        var sqlParams = new
        {
            Email = email
        };

        var exists = await connection.QuerySingleAsync<bool>(
            sqlQuery,
            sqlParams);

        return exists;
    }
}