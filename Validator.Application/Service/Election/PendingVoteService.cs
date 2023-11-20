using Validator.Application.Service.Election;
using Validator.Infrastructure.Repository.Election;
using Validator.Infrastructure.Repository;
using Validator.Application.Handler.Command.Election;
using Validator.Domain;

namespace Validator.Application.Service;

public interface IPendingVoteService
{
    Task CheckAndNotifyVoteLocalCompletion(string sessionElectionId, bool verifyVote, byte[] hash, CancellationToken cancellationToken);
    Task CheckAndNotifyVoteLeaderCompletion(string sessionElectionId, bool verifyVote, byte[] hash, CancellationToken cancellationToken);
}

public class PendingVoteService : IPendingVoteService
{
    private readonly IElectionService _electionService;
    private readonly ISettingRepository _settingRepository;
    private readonly IPendingLocalVoteRepository _pendingLocalVoteRepository;
    private readonly IPendingLeaderVoteRepository _pendingLeaderVoteRepository;

    public PendingVoteService(ISettingRepository settingRepository, IPendingLocalVoteRepository pendingLocalVoteRepository, IPendingLeaderVoteRepository pendingLeaderVoteRepository, IElectionService electionService)
    {
        _electionService = electionService;
        _settingRepository = settingRepository;
        _pendingLocalVoteRepository = pendingLocalVoteRepository;
        _pendingLeaderVoteRepository = pendingLeaderVoteRepository;
    }

    public async Task CheckAndNotifyVoteLeaderCompletion(string sessionElectionId, bool verifyVote, byte[] hash, CancellationToken cancellationToken)
    {
        var resultVoteCounter = await IsVoteCountLeaderGreaterThanThreshold(sessionElectionId, hash, verifyVote, cancellationToken);
        if (!resultVoteCounter)
            return;

        RecordAcceptedVote recordAcceptedVote = new RecordAcceptedVote(hash, verifyVote, sessionElectionId);
        await _electionService.RecordAcceptedVote(recordAcceptedVote, cancellationToken);
    }

    public async Task CheckAndNotifyVoteLocalCompletion(string sessionElectionId, bool verifyVote, byte[] hash, CancellationToken cancellationToken)
    {
        var resultVoteCounter = await IsVoteCountLocalGreaterThanThreshold(sessionElectionId, hash, verifyVote, cancellationToken);
        if (!resultVoteCounter)
            return;

        var notifyLocalVoteVerificationCompleted = new NotifyLocalVoteVerificationCompleted(hash, verifyVote, sessionElectionId);

        await _electionService.NotifyLocalVoteVerificationsCompleted(notifyLocalVoteVerificationCompleted, cancellationToken);
    }

    private async Task<bool> IsVoteCountLocalGreaterThanThreshold(string sessionElectionId, byte[] hash, bool verifyVote, CancellationToken cancellationToken)
    {
        var setting = await _settingRepository.Get("Approver", "AcceptableValidatorsCount", cancellationToken);
        if (setting == null)
            throw new ArgumentNullException("The specified settings were not found.");

        var acceptableCount = Int64.Parse(setting.Value);
        var resultVoteCount = await _pendingLocalVoteRepository.GetCountByHashVerificationAndSession(hash, verifyVote, sessionElectionId, cancellationToken);

        return resultVoteCount >= acceptableCount;
    }

    private async Task<bool> IsVoteCountLeaderGreaterThanThreshold(string sessionElectionId, byte[] hash, bool verifyVote, CancellationToken cancellationToken)
    {
        var setting = await _settingRepository.Get("Approver", "AcceptableValidatorsCount", cancellationToken);
        if (setting == null)
            throw new ArgumentNullException("The specified settings were not found.");

        var acceptableCount = Int64.Parse(setting.Value);
        var resultVoteCount = await _pendingLeaderVoteRepository.GetCountByHashVerificationAndSession(hash, verifyVote, sessionElectionId, cancellationToken);

        return resultVoteCount >= acceptableCount;
    }
}
