using Microsoft.EntityFrameworkCore;
using Validator.Domain.Table.Electronic;
using Validator.Infrastructure.EntityFramework;

namespace Validator.Infrastructure.Repository.Election;

public interface IVoteRecordRepository : IBaseRepository<VoteRecord>
{
    public Task<VoteRecord> GetByVoteProcessId(string sessionElectionId, CancellationToken cancellationToken);
    public Task<IEnumerable<VoteRecord>> GetVoteRecordsByIsInserted(bool isInserted, CancellationToken cancellationToken);
}
public class VoteRecordRepository : GenericRepository<VoteRecord>, IVoteRecordRepository
{
    public VoteRecordRepository(ElectionDatabaseContext validatorDbContext) : base(validatorDbContext) { }

    public async Task<VoteRecord> GetByVoteProcessId(string sessionElectionId, CancellationToken cancellationToken)
    {
        return await ElectionContext.VoteRecords.FirstOrDefaultAsync(a => a.SessionElectionId == sessionElectionId, cancellationToken);
    }

    public async Task<IEnumerable<VoteRecord>> GetVoteRecordsByIsInserted(bool isInserted, CancellationToken cancellationToken)
    {
        return await ElectionContext.VoteRecords.Where(a => a.IsInserted == isInserted).OrderBy(a => a.Id).ToListAsync(cancellationToken);
    }
}