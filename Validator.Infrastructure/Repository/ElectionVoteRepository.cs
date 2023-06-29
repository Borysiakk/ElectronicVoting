using ElectronicVoting.Persistence;
using Validator.Domain.Table.ChangeView;
using ElectronicVoting.Common.Infrastructure;

namespace Validator.Infrastructure.Repository;

public interface IElectionVoteRepository
{

}

public class ElectionVoteRepository : Repository<ElectionVote>, IElectionVoteRepository
{
    public ElectionVoteRepository(ValidatorDbContext dbContext) : base(dbContext) {}
}
