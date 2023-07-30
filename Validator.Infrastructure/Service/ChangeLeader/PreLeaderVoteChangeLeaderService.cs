using Validator.Infrastructure.Repository;
using Validator.Infrastructure.Repository.ChangeLeader;
using Validator.Infrastructure.Service.ChangeLeader.Interface;

namespace Validator.Infrastructure.Service.ChangeLeader;

public interface IPreLeaderVoteChangeLeaderService : IPreVoteChangeLeaderService
{
}

public class PreLeaderVoteChangeLeaderService : IPreLeaderVoteChangeLeaderService
{
    private readonly ISettingRepository _settingRepository;
    private readonly IPreLeaderVoteChangeLeaderRepository _preLeaderVoteChangeLeaderRepository;

    public PreLeaderVoteChangeLeaderService(ISettingRepository settingRepository, IPreLeaderVoteChangeLeaderRepository preLeaderVoteChangeLeaderRepository)
    {
        _settingRepository = settingRepository;
        _preLeaderVoteChangeLeaderRepository = preLeaderVoteChangeLeaderRepository;
    }

    public async Task<bool> IsVoteCountGreaterThanThreshold(string prelectionId, bool decision, CancellationToken cancellationToken)
    {
        var setting = await _settingRepository.Get("Approver", "AcceptableValidatorsCount", cancellationToken);
        if (setting == null)
            throw new ArgumentNullException("The specified settings were not found.");

        var acceptableCount = Int64.Parse(setting.Value);
        var countVote = await _preLeaderVoteChangeLeaderRepository.GetCountByIdAndDecision(prelectionId, decision, cancellationToken);

        if( countVote >= acceptableCount)
            return true;

        return false;
    }
}
