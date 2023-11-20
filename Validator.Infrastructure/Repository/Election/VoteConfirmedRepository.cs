using Microsoft.EntityFrameworkCore;
using Validator.Domain.Comparer;
using Validator.Domain.Table.Electronic;
using Validator.Infrastructure.EntityFramework;

namespace Validator.Infrastructure.Repository.Election;

public interface IVoteConfirmedRepository : IBaseRepository<VoteConfirmed>
{
    Task<List<VoteConfirmed>> GetAndUpdateByInInserted(CancellationToken cancellationToken);
}

public class VoteConfirmedRepository : GenericRepository<VoteConfirmed>, IVoteConfirmedRepository
{
    public VoteConfirmedRepository(ElectionDatabaseContext electionContext) : base(electionContext) { }

    public async Task<List<VoteConfirmed>> GetAndUpdateByInInserted(CancellationToken cancellationToken)
    {
        string sql = @"
        UPDATE VoteConfirmeds
        SET IsInserted = 1
        OUTPUT inserted.*
        WHERE IsInserted = 0";

        var items = await ElectionContext.VoteConfirmeds.FromSqlRaw(sql).ToListAsync(cancellationToken);
        return items.Distinct(new VoteConfirmedBySessionElectionIdComparer()).ToList();
    }
}
