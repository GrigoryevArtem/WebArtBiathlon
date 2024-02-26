using System.Transactions;

namespace ArtBiathlon.Domain.Interfaces.Dal;

public interface IDbRepository
{
    TransactionScope CreateTransactionScope(IsolationLevel level = IsolationLevel.ReadCommitted);
}