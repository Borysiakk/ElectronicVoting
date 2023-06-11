
using System.Data;
using ElectronicVoting.Cammon.Interface;
using ElectronicVoting.Common.Infrastructure;
using ElectronicVoting.Persistence;
using ElectronicVoting.Validator.Domain.Enum;
using ElectronicVoting.Validator.Domain.Table;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace ElectronicVoting.Infrastructure.Repository
{
    public interface IPbftOperationsConsensusRepository
    {
        Task<PbftOperationConsensus?> GetByIdAndStatus(string transactionId, PbftOperationType operationType, CancellationToken cancellationToken);
    }

    public class PbftOperationsConsensusRepository : Repository<PbftOperationConsensus>, IPbftOperationsConsensusRepository, ITransaction
    {
        public PbftOperationsConsensusRepository(ValidatorDbContext dbContext) : base(dbContext){}

        public async Task<IDbContextTransaction> CreateTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.ReadUncommitted, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Database.BeginTransactionAsync(isolationLevel);
        }

        public async Task<List<PbftOperationConsensus>> GetByStatus(PbftOperationStatus status = PbftOperationStatus.NotReady)
        {
            return await _dbSet.Where(a => a.Status == status).ToListAsync();
        }

        public async Task<PbftOperationConsensus?> GetByIdAndStatus(string transactionId, PbftOperationType operationType, CancellationToken cancellationToken)
        {
            return await _dbSet.FirstOrDefaultAsync(a => a.TransactionId == transactionId && a.Operations == operationType, cancellationToken);
        }
    }
}
