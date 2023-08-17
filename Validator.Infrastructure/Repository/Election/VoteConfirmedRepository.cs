using ElectronicVoting.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Validator.Domain.Table.Election;

namespace Validator.Infrastructure.Repository.Election;

public interface IVoteConfirmedRepository : IBaseRepository<VoteConfirmed>
{
    Task<List<VoteConfirmed>> GetAndUpdateByInInserted(CancellationToken cancellationToken);
}

public class VoteConfirmedRepository : GenericRepository<VoteConfirmed>, IVoteConfirmedRepository
{
    public VoteConfirmedRepository(ValidatorDbContext validatorDbContext) : base(validatorDbContext) {}

    public async Task<List<VoteConfirmed>> GetAndUpdateByInInserted(CancellationToken cancellationToken)
    {
        string sql = @"
        UPDATE VotesConfirmed
        SET IsInserted = 1
        OUTPUT inserted.*
        WHERE IsInserted = 0";

        return await _validatorDbContext.VotesConfirmed.FromSqlRaw(sql).ToListAsync(cancellationToken);
    }
}
