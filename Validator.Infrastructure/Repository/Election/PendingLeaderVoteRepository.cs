using Validator.Domain.Table.Electronic;
using Validator.Infrastructure.EntityFramework;
using Validator.Infrastructure.Repository.Election.Base;

namespace Validator.Infrastructure.Repository.Election;

public interface IPendingLeaderVoteRepository : IPendingVoteRepository
{

}
public class PendingLeaderVoteRepository : PendingVoteRepository<PendingLeaderVote>, IPendingLeaderVoteRepository
{
    public PendingLeaderVoteRepository(ElectionDatabaseContext electionContext) : base(electionContext)
    {
    }
}
