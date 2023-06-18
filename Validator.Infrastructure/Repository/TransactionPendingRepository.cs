using ElectronicVoting.Persistence;
using ElectronicVoting.Common.Infrastructure;
using Validator.Domain.Table;

namespace ElectronicVoting.Infrastructure.Repository;

public interface ITransactionPendingRepository
{
    public Int64 CountTransactionPendingByIdAndByHash(string transactionId, byte[] hash);
}
public class TransactionPendingRepository : Repository<TransactionPending>, ITransactionPendingRepository
{
    public TransactionPendingRepository(ValidatorDbContext dbContext) : base(dbContext){}

    public long CountTransactionPendingByIdAndByHash(string transactionId, byte[] hash)
    {
        return _dbSet.Count(a => a.TransactionId == transactionId && a.Hash.SequenceEqual(hash));
    }
}