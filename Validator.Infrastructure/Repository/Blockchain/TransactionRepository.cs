using ElectronicVoting.Persistence;
using Microsoft.EntityFrameworkCore;
using Validator.Domain.Table;

namespace Validator.Infrastructure.Repository.Blockchain;

public interface ITransactionRepository :IBaseRepository<Transaction>
{
    Task<IEnumerable<Transaction>> GetAll(CancellationToken cancellationToken);
    Task<Transaction> GetLast(CancellationToken cancellationToken);
}

public class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
{
    public TransactionRepository(ValidatorDbContext validatorDbContext) : base(validatorDbContext) {}

    public async Task<IEnumerable<Transaction>> GetAll(CancellationToken cancellationToken)
    {
        return await _validatorDbContext.Transactions.ToListAsync(cancellationToken);
    }

    public Task<Transaction> GetLast(CancellationToken cancellationToken)
    {
        return _validatorDbContext.Transactions.OrderBy(a => a.TransactionId).LastOrDefaultAsync(cancellationToken);
    }
}
