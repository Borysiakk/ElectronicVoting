using Validator.Domain.Table.Electronic;
using Validator.Infrastructure.EntityFramework;
using Validator.Infrastructure.Repository.Election.Base;

namespace Validator.Infrastructure.Repository.Election;

public interface IPendingLocalVoteRepository : IPendingVoteRepository
{

}

public sealed class PendingLocalVoteRepository : PendingVoteRepository<PendingLocalVote>, IPendingLocalVoteRepository
{
    public PendingLocalVoteRepository(ElectionDatabaseContext electionContext) : base(electionContext) {}
}
