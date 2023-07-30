using ElectronicVoting.Persistence;
using Validator.Domain.Table.ChangeLeader;
using Validator.Infrastructure.Repository.ChangeLeader.Base;

namespace Validator.Infrastructure.Repository.ChangeLeader;

public interface ILocalVoteChangeLeaderHistoryRepository : IVoteChangeLeaderHistoryRepository
{

}
public class LocalVoteChangeLeaderHistoryRepository : VoteChangeLeaderHistoryRepository<LocalVoteChangeLeaderHistory>, ILocalVoteChangeLeaderHistoryRepository
{
    public LocalVoteChangeLeaderHistoryRepository(ValidatorDbContext validatorDbContext) : base(validatorDbContext)
    {
    }
}
