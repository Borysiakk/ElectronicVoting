using ElectronicVoting.Persistence;
using Microsoft.EntityFrameworkCore;
using Validator.Domain.Table.ChangeLeader.Base;

namespace Validator.Infrastructure.Repository.ChangeLeader.Base
{
    public interface IVoteChangeLeaderHistoryRepository : IBaseRepository<VoteChangeLeaderHistory>
    {
        Task<bool> IsVoteCompleted(string electionId, CancellationToken cancellationToken);
    }
    public class VoteChangeLeaderHistoryRepository<T> : GenericRepository<VoteChangeLeaderHistory>, IVoteChangeLeaderHistoryRepository where T : VoteChangeLeaderHistory
    {
        public VoteChangeLeaderHistoryRepository(ValidatorDbContext validatorDbContext) : base(validatorDbContext){}

        public async Task<bool> IsVoteCompleted(string electionId, CancellationToken cancellationToken)
        {
            return await _validatorDbContext.Set<T>().AnyAsync(a => a.ElectionChangeLeaderId == electionId, cancellationToken);
        }
    }
}
