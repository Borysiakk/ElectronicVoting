using ElectronicVoting.Persistence;
using Validator.Domain.Table.Election;
using Validator.Infrastructure.Repository.Election.Base;

namespace Validator.Infrastructure.Repository.Election;

public interface IPendingLocalVoteRepository : IPendingVoteRepository
{

}

public class PendingLocalVoteRepository : PendingVoteRepository<PendingLocalVote>, IPendingLocalVoteRepository
{
    public PendingLocalVoteRepository(ValidatorDbContext validatorDbContext) : base(validatorDbContext) {}
}
