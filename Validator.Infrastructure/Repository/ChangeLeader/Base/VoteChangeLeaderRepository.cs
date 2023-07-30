using ElectronicVoting.Persistence;
using Microsoft.EntityFrameworkCore;
using Validator.Domain.Table.ChangeLeader.Base;

namespace Validator.Infrastructure.Repository.ChangeLeader.Base;

public interface IVoteChangeLeaderRepository : IBaseRepository<VoteChangeLeader>
{
    Task<long> GetCountByIdAndVote(string electionId, Int64 vote, CancellationToken cancellationToken);
}

public class VoteChangeLeaderRepository<T> : GenericRepository<VoteChangeLeader>, IVoteChangeLeaderRepository where T : VoteChangeLeader
{
    public VoteChangeLeaderRepository(ValidatorDbContext validatorDbContext) : base(validatorDbContext) {}

    public async Task<Int64> GetCountByIdAndVote(string electionChangeLeaderId, long vote, CancellationToken cancellationToken)
    {
        return await _validatorDbContext.Set<T>().LongCountAsync(a=>a.ElectionChangeLeaderId == electionChangeLeaderId && a.Vote == vote, cancellationToken);
    }
}
