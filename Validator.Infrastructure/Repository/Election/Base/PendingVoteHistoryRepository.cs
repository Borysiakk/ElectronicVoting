using ElectronicVoting.Persistence;
using Microsoft.EntityFrameworkCore;
using Validator.Domain.Table.Election.Base;

namespace Validator.Infrastructure.Repository.Election.Base;

public interface IPendingVoteHistoryRepository :IBaseRepository<PendingVoteHistory>
{
    Task<bool> IsVoteCompleted(string voteProcessId, CancellationToken cancellationToken);
}

public abstract class PendingVoteHistoryRepository<T> : GenericRepository<PendingVoteHistory>, IPendingVoteHistoryRepository where T : PendingVoteHistory
{
    public PendingVoteHistoryRepository(ValidatorDbContext validatorDbContext) : base(validatorDbContext) {}

    public Task<bool> IsVoteCompleted(string voteProcessId, CancellationToken cancellationToken)
    {
        return _validatorDbContext.Set<T>().AnyAsync(a=>a.VoteProcessId == voteProcessId, cancellationToken);
    }
}
