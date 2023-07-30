using ElectronicVoting.Persistence;
using Validator.Domain.Table.ChangeLeader;
using Validator.Infrastructure.Repository.ChangeLeader.Base;

namespace Validator.Infrastructure.Repository.ChangeLeader;

public interface ILeaderVoteChangeLeaderHistoryRepository : IVoteChangeLeaderHistoryRepository
{

}

public class LeaderVoteChangeLeaderHistoryRepository : VoteChangeLeaderHistoryRepository<LeaderVoteChangeLeaderHistory>, ILeaderVoteChangeLeaderHistoryRepository
{
    public LeaderVoteChangeLeaderHistoryRepository(ValidatorDbContext validatorDbContext) : base(validatorDbContext) {}
}
