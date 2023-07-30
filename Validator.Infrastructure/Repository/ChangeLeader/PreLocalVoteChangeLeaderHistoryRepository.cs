using ElectronicVoting.Persistence;
using Validator.Domain.Table.ChangeLeader;
using Validator.Infrastructure.Repository.ChangeLeader.Base;

namespace Validator.Infrastructure.Repository.ChangeLeader;

public interface IPreLocalVoteChangeLeaderHistoryRepository : IPreVoteChangeLeaderHistoryRepository
{

}

public class PreLocalVoteChangeLeaderHistoryRepository : PreVoteChangeLeaderdHistoryRepository<PreLocalVoteChangeLeaderHistory>, IPreLocalVoteChangeLeaderHistoryRepository
{
    public PreLocalVoteChangeLeaderHistoryRepository(ValidatorDbContext validatorDbContext) : base(validatorDbContext) { }
}
