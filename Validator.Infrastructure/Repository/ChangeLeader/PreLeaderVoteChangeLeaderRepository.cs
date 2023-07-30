using ElectronicVoting.Persistence;
using Validator.Domain.Table.ChangeLeader;
using Validator.Infrastructure.Repository.ChangeLeader.Base;

namespace Validator.Infrastructure.Repository.ChangeLeader;

public interface IPreLeaderVoteChangeLeaderRepository : IPreVoteChangeLeaderRepository { }

public class PreLeaderVoteChangeLeaderRepository : PreVoteChangeLeaderRepository<PreLeaderVoteChangeLeader>, IPreLeaderVoteChangeLeaderRepository
{
    public PreLeaderVoteChangeLeaderRepository(ValidatorDbContext validatorDbContext) : base(validatorDbContext) { }
}
