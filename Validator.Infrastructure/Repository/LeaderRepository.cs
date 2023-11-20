using Microsoft.EntityFrameworkCore;
using Validator.Domain.Table.Electronic;
using Validator.Infrastructure.EntityFramework;

namespace Validator.Infrastructure.Repository
{
    public interface ILeaderRepository
    {
        Task<long> GetApproverIdForLatestLeader(CancellationToken cancellationToken);
    }

    public sealed class LeaderRepository : GenericRepository<Leader>, ILeaderRepository
    {

        public LeaderRepository(ElectionDatabaseContext electionContext) : base(electionContext) { }

        public async Task<long> GetApproverIdForLatestLeader(CancellationToken cancellationToken)
        {
            return await ElectionContext.Leaders.MaxAsync(a => a.LeaderId, cancellationToken);
        }
    }
}
