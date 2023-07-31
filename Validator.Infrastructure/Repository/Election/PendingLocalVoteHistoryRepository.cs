using ElectronicVoting.Persistence;
using Validator.Domain.Table.Election;
using Validator.Infrastructure.Repository.Election.Base;

namespace Validator.Infrastructure.Repository.Election;

public interface IPendingLocalVoteHistoryRepository : IPendingVoteHistoryRepository
{

}

public class PendingLocalVoteHistoryRepository : PendingVoteHistoryRepository<PendingLocalVoteHistory>, IPendingLocalVoteHistoryRepository
{
    public PendingLocalVoteHistoryRepository(ValidatorDbContext validatorDbContext) : base(validatorDbContext){}

}
