using Microsoft.EntityFrameworkCore;
using Validator.Domain.Table.Blockchain;
using Validator.Infrastructure.EntityFramework;

namespace Validator.Infrastructure.Repository.Blockchain;

public interface ITransactionRepository : IBaseRepository<Transaction>
{
    Task<IEnumerable<Transaction>> GetAll(CancellationToken cancellationToken);
    Task<Transaction> GetLast(CancellationToken cancellationToken);
}

public class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
{
    public TransactionRepository(ElectionDatabaseContext electionDatabaseContext) : base(electionDatabaseContext) { }

    public async Task<IEnumerable<Transaction>> GetAll(CancellationToken cancellationToken)
    {
        return await ElectionContext.Transactions.ToListAsync(cancellationToken);
    }

    public Task<Transaction> GetLast(CancellationToken cancellationToken)
    {
        return ElectionContext.Transactions.OrderBy(a => a.TransactionId).LastOrDefaultAsync(cancellationToken);
    }
}
