using Microsoft.EntityFrameworkCore;
using Validator.Domain.Table.Electronic.Base;
using Validator.Infrastructure.EntityFramework;

namespace Validator.Infrastructure.Repository.Election.Base;
public interface IPendingVoteRepository :IBaseRepository<PendingVoteBase>
{
    Task<long> GetCountByHashVerificationAndSession (byte[] hash, bool verifyVote, string sessionElectionId, CancellationToken cancellationToken);
}

public abstract class PendingVoteRepository<T> : GenericRepository<PendingVoteBase>, IPendingVoteRepository where T : PendingVoteBase
{
    protected PendingVoteRepository(ElectionDatabaseContext electionContext) : base(electionContext) {}

    public async Task<long> GetCountByHashVerificationAndSession(byte[] hash, bool verifyVote, string sessionElectionId, CancellationToken cancellationToken)
    {
        return await ElectionContext.Set<T>().LongCountAsync(a => a.SessionElectionId == sessionElectionId && a.Hash.SequenceEqual(hash), cancellationToken);
    }
}
