using ElectronicVoting.Persistence;
using Microsoft.EntityFrameworkCore;
using Validator.Domain.Table.ChangeLeader;

namespace Validator.Infrastructure.Repository;

public interface ILeaderRepository :IBaseRepository<Leader>
{
    Task<Int64> GetApproverIdForLatestLeader(CancellationToken cancellationToken);
}

public class LeaderRepository : GenericRepository<Leader>, ILeaderRepository
{
    public LeaderRepository(ValidatorDbContext validatorDbContext) : base(validatorDbContext) {}

    public async Task<Int64> GetApproverIdForLatestLeader(CancellationToken cancellationToken)
    {
        return await _validatorDbContext.Leaders.MaxAsync(a => a.LeaderId, cancellationToken);
    }

}
