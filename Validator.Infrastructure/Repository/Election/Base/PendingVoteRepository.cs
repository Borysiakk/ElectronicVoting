using ElectronicVoting.Persistence;
using Microsoft.EntityFrameworkCore;
using Validator.Domain.Table.Election;

namespace Validator.Infrastructure.Repository.Election.Base;

public interface IPendingVoteRepository: IBaseRepository<PendingVote>
{
    Task<Int64> GetCountByVoteProcessIdAndVoteHash(string voteProcessId, byte[] hash, CancellationToken cancellationToken);
}

public abstract class PendingVoteRepository<T> : GenericRepository<PendingVote>, IPendingVoteRepository where T : PendingVote
{
    public PendingVoteRepository(ValidatorDbContext validatorDbContext) : base(validatorDbContext) { }

    public async Task<Int64> GetCountByVoteProcessIdAndVoteHash(string voteProcessId, byte[] hash, CancellationToken cancellationToken)
    {
        return await _validatorDbContext.Set<T>().LongCountAsync(a => a.VoteProcessId == voteProcessId && a.Hash.SequenceEqual(hash), cancellationToken);
    }
}
