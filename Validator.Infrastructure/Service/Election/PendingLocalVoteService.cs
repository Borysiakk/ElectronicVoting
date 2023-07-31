using Validator.Infrastructure.Repository;
using Validator.Infrastructure.Repository.Election;
namespace Validator.Infrastructure.Service.Election;

public interface IPendingLocalVoteService
{
    Task<bool> IsVoteCountGreaterThanThreshold(string voteProcessId, byte[] hash, CancellationToken cancellationToken);
}

public class PendingLocalVoteService : IPendingLocalVoteService
{
    private readonly ISettingRepository _settingRepository;
    private readonly IPendingLocalVoteRepository _pendingLocalVoteRepository;

    public PendingLocalVoteService(ISettingRepository settingRepository, IPendingLocalVoteRepository pendingLocalVoteRepository)
    {
        _settingRepository = settingRepository;
        _pendingLocalVoteRepository = pendingLocalVoteRepository;
    }

    public async Task<bool> IsVoteCountGreaterThanThreshold(string voteProcessId, byte[] hash, CancellationToken cancellationToken)
    {
        var setting = await _settingRepository.Get("Approver", "AcceptableValidatorsCount", cancellationToken);
        if (setting == null)
            throw new ArgumentNullException("The specified settings were not found.");

        var acceptableCount = Int64.Parse(setting.Value);
        var resultVoteCount = await _pendingLocalVoteRepository.GetCountByVoteProcessIdAndVoteHash(voteProcessId, hash, cancellationToken);
        

        return resultVoteCount >= acceptableCount;
    }
}
