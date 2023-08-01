using Validator.Infrastructure.Repository;
using Validator.Infrastructure.Repository.Election;

namespace Validator.Infrastructure.Service.Election;

public interface IPendingLeaderVoteService
{
    Task<bool> IsVoteCountGreaterThanThreshold(string voteProcessId, byte[] hash, CancellationToken cancellationToken);
}

public class PendingLeaderVoteService : IPendingLeaderVoteService
{
    private readonly ISettingRepository _settingRepository;
    private readonly IPendingLeaderVoteRepository _pendingLeaderVoteRepository;

    public PendingLeaderVoteService(ISettingRepository settingRepository, IPendingLeaderVoteRepository pendingLeaderVoteRepository)
    {
        _settingRepository = settingRepository;
        _pendingLeaderVoteRepository = pendingLeaderVoteRepository;
    }

    public async Task<bool> IsVoteCountGreaterThanThreshold(string voteProcessId, byte[] hash, CancellationToken cancellationToken)
    {
        var setting = await _settingRepository.Get("Approver", "AcceptableValidatorsCount", cancellationToken);
        if (setting == null)
            throw new ArgumentNullException("The specified settings were not found.");

        var acceptableCount = Int64.Parse(setting.Value);
        var resultVoteCount = await _pendingLeaderVoteRepository.GetCountByVoteProcessIdAndVoteHash(voteProcessId, hash, cancellationToken);


        return resultVoteCount >= acceptableCount;
    }
}
