using ElectronicVoting.Persistence;
using Microsoft.EntityFrameworkCore;
using Validator.Domain.Table.ChangeLeader.Base;

namespace Validator.Infrastructure.Repository.ChangeLeader.Base;

public interface IPreVoteChangeLeaderHistoryRepository : IBaseRepository<PreVoteChangeLeaderHistory>
{
    Task<bool> IsVoteCompleted(string preElectionId, CancellationToken cancellationToken);
}

public class PreVoteChangeLeaderdHistoryRepository<T> : GenericRepository<PreVoteChangeLeaderHistory>, IPreVoteChangeLeaderHistoryRepository where T : PreVoteChangeLeaderHistory
{

    public PreVoteChangeLeaderdHistoryRepository(ValidatorDbContext validatorDbContext) : base(validatorDbContext){}

    public async Task<bool> IsVoteCompleted(string preElectionChangeLeaderId, CancellationToken cancellationToken)
    {
        return await _validatorDbContext.Set<T>().AnyAsync(a => a.PreElectionChangeLeaderId == preElectionChangeLeaderId, cancellationToken);
    }
}
