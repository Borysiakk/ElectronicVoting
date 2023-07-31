using ElectronicVoting.Persistence;
using Microsoft.EntityFrameworkCore;
using Validator.Domain.Table.ChangeLeader.Base;

namespace Validator.Infrastructure.Repository.ChangeLeader.Base;

public interface IPreVoteChangeLeaderRepository : IBaseRepository<PreVoteChangeLeader>
{
    Task<Int64> GetCountByIdAndDecision(string preElectionId, bool decision, CancellationToken cancellationToken);
}

public abstract class PreVoteChangeLeaderRepository<T> : GenericRepository<PreVoteChangeLeader>, IPreVoteChangeLeaderRepository where T : PreVoteChangeLeader
{
    public PreVoteChangeLeaderRepository(ValidatorDbContext validatorDbContext) : base(validatorDbContext) { }

    public async Task<Int64> GetCountByIdAndDecision(string preElectionId, bool decision, CancellationToken cancellationToken)
    {
        return await _validatorDbContext.Set<T>().LongCountAsync(a => a.PreElectionChangeLeaderId == preElectionId && a.Decision == decision, cancellationToken);
    }
}
