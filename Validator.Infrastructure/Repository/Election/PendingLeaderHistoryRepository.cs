using ElectronicVoting.Persistence;
using Validator.Domain.Table.Election;
using Validator.Infrastructure.Repository.Election.Base;

namespace Validator.Infrastructure.Repository.Election;

public interface IPendingLeaderHistoryRepository : IPendingVoteHistoryRepository
{

}

public class PendingLeaderHistoryRepository : PendingVoteHistoryRepository<PendingLeaderVoteHistory>, IPendingLeaderHistoryRepository
{
    public PendingLeaderHistoryRepository(ValidatorDbContext validatorDbContext) : base(validatorDbContext) {}
}
