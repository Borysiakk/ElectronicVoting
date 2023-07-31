using ElectronicVoting.Persistence;
using Validator.Domain.Table.Election;
using Validator.Infrastructure.Repository.Election.Base;

namespace Validator.Infrastructure.Repository.Election;
public interface IPendingLeaderVoteRepository : IPendingVoteRepository
{

}
public class PendingLeaderVoteRepository : PendingVoteRepository<PendingLeaderVote>, IPendingLeaderVoteRepository
{
    public PendingLeaderVoteRepository(ValidatorDbContext validatorDbContext) : base(validatorDbContext) {}
}
