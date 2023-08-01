using ElectronicVoting.Persistence;
using Microsoft.EntityFrameworkCore;
using Validator.Domain.Table.Election;

namespace Validator.Infrastructure.Repository.Election;

public interface IVoteConfirmedRepository : IBaseRepository<VoteConfirmed>
{
    Task<IEnumerable<VoteConfirmed>> GetVoteConfirmationsInInsertionOrder(CancellationToken cancellationToken);
}

public class VoteConfirmedRepository : GenericRepository<VoteConfirmed>, IVoteConfirmedRepository
{
    public VoteConfirmedRepository(ValidatorDbContext validatorDbContext) : base(validatorDbContext) {}

    public async Task<IEnumerable<VoteConfirmed>> GetVoteConfirmationsInInsertionOrder(CancellationToken cancellationToken)
    {
        return await _validatorDbContext.VoteRecords.Join(_validatorDbContext.VotesConfirmed, voteRecord => voteRecord.VoteProcessId, voteConfirmed => voteConfirmed.VoteProcessId, (voteRecord, voteConfirmed) => new
        {
            VoteRecord = voteRecord,
            VoteConfirmed = voteConfirmed
        })
        .Where(x => x.VoteConfirmed.IsInserted == false).OrderBy(y => y.VoteRecord.Id).Select(z => z.VoteConfirmed).ToListAsync(cancellationToken);
    }
}
