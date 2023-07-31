using EntityFrameworkCore.Triggered;
using Validator.Domain;
using Validator.Domain.Table.Election;
using Validator.Infrastructure.Handler.Command.Election;
using Validator.Infrastructure.Repository.Election;
using Validator.Infrastructure.Service;
using Validator.Infrastructure.Service.Election;

namespace Validator.API.Triggers.Election;

public class ElectionLocalThresholdTrigger : IAfterSaveTrigger<PendingLocalVote>
{
    private readonly IApproverService _approverService;
    private readonly IPendingLocalVoteService _pendingLocalVoteService;
    private readonly IPendingLocalVoteHistoryRepository _pendingLocalVoteHistoryRepository;

    public ElectionLocalThresholdTrigger(IApproverService approverService, IPendingLocalVoteService pendingLocalVoteService, IPendingLocalVoteHistoryRepository pendingLocalVoteHistoryRepository)
    {
        _approverService = approverService;
        _pendingLocalVoteService = pendingLocalVoteService;
        _pendingLocalVoteHistoryRepository = pendingLocalVoteHistoryRepository;
    }

    public async Task AfterSave(ITriggerContext<PendingLocalVote> context, CancellationToken cancellationToken)
    {
        var addedItem = context.Entity;

        var isVoteCompleted = await _pendingLocalVoteHistoryRepository.IsVoteCompleted(addedItem.VoteProcessId, cancellationToken);
        if (isVoteCompleted)
            return;

        var resultVoteCounter =  await _pendingLocalVoteService.IsVoteCountGreaterThanThreshold(addedItem.VoteProcessId, addedItem.Hash, cancellationToken);
        if (!resultVoteCounter)
            return;

        var notifyLocalVotingCompleted = new NotifyLocalVotingCompleted()
        {
            Hash = addedItem.Hash,
            VoteProcessId = addedItem.VoteProcessId,
        };

        var pendingLocalVoteHistory = new PendingLocalVoteHistory()
        {
            VoteProcessId = addedItem.VoteProcessId,
        };


        await _pendingLocalVoteHistoryRepository.Add(pendingLocalVoteHistory, cancellationToken);
        await _approverService.SendPostToLeaderApprover(Routes.NotifyLocalVotingCompleted, notifyLocalVotingCompleted, cancellationToken);
    }
}
