using Microsoft.AspNetCore.Routing;
using Validator.Application.Handler.Command.Election;
using Validator.Domain;
using Validator.Infrastructure.Repository;
using Validator.Infrastructure.Repository.Election;

namespace Validator.Application.Service.Election;

public interface IPendingLocalVoteService
{
    Task CheckAndNotifyVoteCompletion(string sessionElectionId, bool verifyVote, byte[] hash, CancellationToken cancellationToken);
}
public class PendingLocalVoteService : IPendingLocalVoteService
{

    private readonly IApproverService _approverService;
    private readonly ISettingRepository _settingRepository;
    private readonly IPendingLocalVoteRepository _pendingLocalVoteRepository;

    public PendingLocalVoteService(IPendingLocalVoteRepository pendingLocalVoteRepository, ISettingRepository settingRepository, IApproverService approverService)
    {
        _settingRepository = settingRepository;
        _pendingLocalVoteRepository = pendingLocalVoteRepository;
        _approverService = approverService;
    }

    public async Task CheckAndNotifyVoteCompletion(string sessionElectionId, bool verifyVote, byte[] hash, CancellationToken cancellationToken)
    {
        var resultVoteCounter = await IsVoteCountGreaterThanThreshold(sessionElectionId, hash, verifyVote, cancellationToken);
        var notifyLocalVoteVerificationCompleted = new NotifyLocalVoteVerificationCompleted(hash, verifyVote, sessionElectionId);

        await _approverService.SendPostToLeaderApprover(Routes.NotifyLocalVoteVerificationCompleted, notifyLocalVoteVerificationCompleted, cancellationToken);

    }

    private async Task<bool> IsVoteCountGreaterThanThreshold(string sessionElectionId, byte[] hash, bool verifyVote, CancellationToken cancellationToken)
    {
        var setting = await _settingRepository.Get("Approver", "AcceptableValidatorsCount", cancellationToken);
        if (setting == null)
            throw new ArgumentNullException("The specified settings were not found.");

        var acceptableCount = Int64.Parse(setting.Value);
        var resultVoteCount = await _pendingLocalVoteRepository.GetCountByHashVerificationAndSession(hash, verifyVote, sessionElectionId, cancellationToken);

        return resultVoteCount >= acceptableCount;
    }
}
