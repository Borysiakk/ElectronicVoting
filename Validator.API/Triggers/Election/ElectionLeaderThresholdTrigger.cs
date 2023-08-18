using EntityFrameworkCore.Triggered;
using Validator.Domain;
using Validator.Domain.Table.Election;
using Validator.Infrastructure.Handler.Command.Election;
using Validator.Infrastructure.Repository.Election;
using Validator.Infrastructure.Service;
using Validator.Infrastructure.Service.Election;

namespace Validator.API.Triggers.Election;

public class ElectionLeaderThresholdTrigger : IAfterSaveTrigger<PendingLeaderVote>
{
    private readonly IApproverService _approverService;
    private readonly IPendingLeaderVoteService _pendingLeaderVoteService;
    private readonly IPendingLeaderHistoryRepository _pendingLeaderHistoryRepository;

    public ElectionLeaderThresholdTrigger(IApproverService approverService, IPendingLeaderVoteService pendingLeaderVoteService, IPendingLeaderHistoryRepository pendingLeaderHistoryRepository)
    {
        _approverService = approverService;
        _pendingLeaderVoteService = pendingLeaderVoteService;
        _pendingLeaderHistoryRepository = pendingLeaderHistoryRepository;
    }

    public async Task AfterSave(ITriggerContext<PendingLeaderVote> context, CancellationToken cancellationToken)
    {
        if (context.ChangeType == ChangeType.Added)
        {
            var includeSender = true;
            var addedItem = context.Entity;

            var isVoteCompleted = await _pendingLeaderHistoryRepository.IsVoteCompleted(addedItem.VoteProcessId, cancellationToken);
            if (isVoteCompleted)
                return;

            var resultVoteCounter = await _pendingLeaderVoteService.IsVoteCountGreaterThanThreshold(addedItem.VoteProcessId, addedItem.Hash, cancellationToken);
            if (!resultVoteCounter)
                return;

            var recordAcceptedVote = new RecordAcceptedVote(addedItem.VoteProcessId, addedItem.Hash);
            var pendingLeaderVoteHistory = new PendingLeaderVoteHistory(addedItem.VoteProcessId);

            await _pendingLeaderHistoryRepository.Add(pendingLeaderVoteHistory, cancellationToken);
            await _approverService.SendPostToApprovers(Routes.RecordAcceptedVote, recordAcceptedVote, includeSender, cancellationToken);
        }
    }
}
