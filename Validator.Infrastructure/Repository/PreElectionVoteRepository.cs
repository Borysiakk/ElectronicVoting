
using ElectronicVoting.Common.Infrastructure;
using ElectronicVoting.Persistence;
using Microsoft.EntityFrameworkCore;
using Validator.Domain.Table.ChangeView;

namespace Validator.Infrastructure.Repository;
public interface IPreElectionVoteRepository
{
    Task<int> GetCountByTransactionIdAndDecision(string transactionId, CancellationToken cancellationToken);
}
public class PreElectionVoteRepository : Repository<PreElectionVote>, IPreElectionVoteRepository
{
    public PreElectionVoteRepository(ValidatorDbContext dbContext) : base(dbContext) {}

    public async Task<int> GetCountByTransactionIdAndDecision(string transactionId, CancellationToken cancellationToken)
    {
        return await _dbSet.CountAsync(a=>a.TransactionId == transactionId && a.Decision == true, cancellationToken);
    }
} 
