using ElectronicVoting.Persistence;
using Validator.Domain.Table.ChangeLeader;
using Validator.Infrastructure.Repository.ChangeLeader.Base;

namespace Validator.Infrastructure.Repository.ChangeLeader;
public interface ILocalVoteChangeLeaderRepository : IVoteChangeLeaderRepository
{

}

public class LocalVoteChangeLeaderRepository : VoteChangeLeaderRepository<LocalVoteChangeLeader>, ILocalVoteChangeLeaderRepository
{
    public LocalVoteChangeLeaderRepository(ValidatorDbContext validatorDbContext) : base(validatorDbContext) {}
}
