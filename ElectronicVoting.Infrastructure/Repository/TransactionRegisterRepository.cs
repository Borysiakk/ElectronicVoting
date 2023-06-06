using ElectronicVoting.Domain.Table;
using ElectronicVoting.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace ElectronicVoting.Infrastructure.Repository
{
    public interface ITransactionRegisterRepository
    {
        public Task<List<TransactionRegister>> GetByIsInserted(bool isInserted, CancellationToken cancellationToken);
    }

    public class TransactionRegisterRepository : Repository<TransactionRegister>, ITransactionRegisterRepository, ITransaction
    {
        public TransactionRegisterRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IDbContextTransaction> CreateTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.ReadUncommitted, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Database.BeginTransactionAsync(isolationLevel);
        }

        public async Task<List<TransactionRegister>> GetByIsInserted(bool isInserted, CancellationToken cancellationToken)
        {
            return await _dbSet.Where(a => a.IsInserted == isInserted).ToListAsync(cancellationToken);
        }
    }
}
