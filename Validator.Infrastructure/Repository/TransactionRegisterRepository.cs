using ElectronicVoting.Cammon.Interface;
using ElectronicVoting.Common.Infrastructure;
using ElectronicVoting.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using Validator.Domain.Table;

namespace ElectronicVoting.Infrastructure.Repository
{
    public interface ITransactionRegisterRepository
    {
        public Task<List<TransactionRegister>> GetByIsInserted(bool isInserted, CancellationToken cancellationToken);
    }

    public class TransactionRegisterRepository : Repository<TransactionRegister>, ITransactionRegisterRepository, ITransaction
    {
        public TransactionRegisterRepository(ValidatorDbContext dbContext) : base(dbContext) {}

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
