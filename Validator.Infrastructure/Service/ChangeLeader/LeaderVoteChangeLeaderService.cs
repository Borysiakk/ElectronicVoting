
using Validator.Infrastructure.Repository;
using Validator.Infrastructure.Repository.ChangeLeader;
using Validator.Infrastructure.Service.ChangeLeader.Interface;

namespace Validator.Infrastructure.Service.ChangeLeader;

public interface ILeaderVoteChangeLeaderService : IVoteChangeLeaderService
{

}

public class LeaderVoteChangeLeaderService : ILeaderVoteChangeLeaderService
{
    private readonly ISettingRepository _settingRepository;
    private readonly ILeaderVoteChangeLeaderRepository _leaderVoteChangeLeaderRepository;

    public LeaderVoteChangeLeaderService(ISettingRepository settingRepository, ILeaderVoteChangeLeaderRepository leaderVoteChangeLeaderRepository)
    {
        _settingRepository = settingRepository;
        _leaderVoteChangeLeaderRepository = leaderVoteChangeLeaderRepository;
    }

    public async Task<bool> IsVoteCountGreaterThanThreshold(string electionId, long vote, CancellationToken cancellationToken)
    {
        var setting = await _settingRepository.Get("Approver", "AcceptableValidatorsCount", cancellationToken);
        if (setting == null)
            throw new ArgumentNullException("The specified settings were not found.");

        var acceptableCount = Int64.Parse(setting.Value);

        var countVote = await _leaderVoteChangeLeaderRepository.GetCountByIdAndVote(electionId, vote, cancellationToken);

        if (countVote >= acceptableCount)
            return true;

        return false;
    }
}
