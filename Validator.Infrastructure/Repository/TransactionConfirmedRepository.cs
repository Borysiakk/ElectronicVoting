using ElectronicVoting.Common.Infrastructure;
using ElectronicVoting.Persistence;
using ElectronicVoting.Validator.Domain.Table;
using Microsoft.EntityFrameworkCore;

namespace ElectronicVoting.Infrastructure.Repository
{
    public interface ITransactionConfirmedRepository
    {
        public Task<bool> IsExistsTransactionConfirmedById(string transactionId);
        public Task<List<TransactionConfirmed>> GetByIsInserted(bool isInserted, CancellationToken cancellationToken);
    }

    public class TransactionConfirmedRepository : Repository<TransactionConfirmed>, ITransactionConfirmedRepository
    {
        public TransactionConfirmedRepository(ValidatorDbContext dbContext) : base(dbContext) {}

        public async Task<List<TransactionConfirmed>> GetByIsInserted(bool isInserted, CancellationToken cancellationToken)
        {
            return await _dbSet.Where(a => a.IsInserted == isInserted).ToListAsync(cancellationToken);
        }

        public async Task<bool> IsExistsTransactionConfirmedById(string transactionId)
        {
            return await _dbSet.AnyAsync(a => a.TransactionId == transactionId);
        }
    }
}
