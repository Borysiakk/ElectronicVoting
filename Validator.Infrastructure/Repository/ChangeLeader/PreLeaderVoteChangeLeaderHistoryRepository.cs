using ElectronicVoting.Persistence;
using Validator.Domain.Table.ChangeLeader;
using Validator.Infrastructure.Repository.ChangeLeader.Base;

namespace Validator.Infrastructure.Repository.ChangeLeader;

public interface IPreLeaderVoteChangeLeaderHistoryRepository : IPreVoteChangeLeaderHistoryRepository
{

}

public class PreLeaderVoteChangeLeaderHistoryRepository : PreVoteChangeLeaderdHistoryRepository<PreLeaderVoteChangeLeaderHistory>, IPreLeaderVoteChangeLeaderHistoryRepository
{
    public PreLeaderVoteChangeLeaderHistoryRepository(ValidatorDbContext validatorDbContext) : base(validatorDbContext) { }
}
