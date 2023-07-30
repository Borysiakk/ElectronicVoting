using ElectronicVoting.Persistence;
using Validator.Domain.Table.ChangeLeader;
using Validator.Infrastructure.Repository.ChangeLeader.Base;

namespace Validator.Infrastructure.Repository.ChangeLeader;

public interface ILeaderVoteChangeLeaderRepository : IVoteChangeLeaderRepository
{

}

public class LeaderVoteChangeLeaderRepository : VoteChangeLeaderRepository<LeaderVoteChangeLeader>, ILeaderVoteChangeLeaderRepository
{
    public LeaderVoteChangeLeaderRepository(ValidatorDbContext validatorDbContext) : base(validatorDbContext) {}
}
