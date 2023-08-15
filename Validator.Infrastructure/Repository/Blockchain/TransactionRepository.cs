using ElectronicVoting.Persistence;
using Microsoft.EntityFrameworkCore;
using Validator.Domain.Table;

namespace Validator.Infrastructure.Repository.Blockchain;

public interface ITransactionRepository :IBaseRepository<Transaction>
{
    Task<Transaction> GetLast(CancellationToken cancellationToken);
}

public class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
{
    public TransactionRepository(ValidatorDbContext validatorDbContext) : base(validatorDbContext) {}

    public Task<Transaction> GetLast(CancellationToken cancellationToken)
    {
        return _validatorDbContext.Transactions.OrderBy(a => a.TransactionId).LastOrDefaultAsync(cancellationToken);
    }
}
