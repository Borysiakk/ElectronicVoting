using ElectronicVoting.Persistence;
using Microsoft.EntityFrameworkCore;
using Validator.Domain.Table.Election;

namespace Validator.Infrastructure.Repository.Election;

public interface IVoteRecordRepository : IBaseRepository<VoteRecord>
{
    public Task<VoteRecord> GetByVoteProcessId(string voteProcessId,CancellationToken cancellationToken);
    public Task<IEnumerable<VoteRecord>> GetVoteRecordsByIsInserted(bool isInserted, CancellationToken cancellationToken);
}
internal class VoteRecordRepository : GenericRepository<VoteRecord>, IVoteRecordRepository
{
    public VoteRecordRepository(ValidatorDbContext validatorDbContext) : base(validatorDbContext) {}

    public async Task<VoteRecord> GetByVoteProcessId(string voteProcessId, CancellationToken cancellationToken)
    {
        return await _validatorDbContext.VoteRecords.FirstOrDefaultAsync(a => a.VoteProcessId == voteProcessId, cancellationToken);
    }

    public async Task<IEnumerable<VoteRecord>> GetVoteRecordsByIsInserted(bool isInserted, CancellationToken cancellationToken)
    {
        return await _validatorDbContext.VoteRecords.Where(a => a.IsInserted == isInserted).OrderBy(a=>a.Id).ToListAsync(cancellationToken);
    }
}
