using ElectronicVoting.Persistence;
using Validator.Domain.Table.ChangeLeader;
using Validator.Infrastructure.Repository.ChangeLeader.Base;

namespace Validator.Infrastructure.Repository.ChangeLeader;
public interface IPreLocalVoteChangeLeaderRepository : IPreVoteChangeLeaderRepository { }

public class PreLocalVoteChangeLeaderRepository : PreVoteChangeLeaderRepository<PreLocalVoteChangeLeader>, IPreLocalVoteChangeLeaderRepository
{
    public PreLocalVoteChangeLeaderRepository(ValidatorDbContext validatorDbContext) : base(validatorDbContext) { }
}
