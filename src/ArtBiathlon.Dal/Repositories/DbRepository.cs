using System.Transactions;
using ArtBiathlon.Dal.Settings;
using ArtBiathlon.Domain.Interfaces.Dal;
using Npgsql;

namespace ArtBiathlon.Dal.Repositories;

internal abstract class DbRepository : IDbRepository
{
    private readonly DalOptions _dalSettings;

    protected DbRepository(DalOptions dalSettings)
    {
        _dalSettings = dalSettings;
    }

    public TransactionScope CreateTransactionScope(
        IsolationLevel level = IsolationLevel.ReadCommitted)
    {
        return new TransactionScope(
            TransactionScopeOption.Required,
            new TransactionOptions
            {
                IsolationLevel = level,
                Timeout = TimeSpan.FromSeconds(5)
            },
            TransactionScopeAsyncFlowOption.Enabled);
    }

    protected async Task<NpgsqlConnection> GetAndOpenConnection(CancellationToken token)
    {
        var connection = new NpgsqlConnection(_dalSettings.ConnectionString);
        await connection.OpenAsync(token);
        connection.ReloadTypes();
        return connection;
    }
}