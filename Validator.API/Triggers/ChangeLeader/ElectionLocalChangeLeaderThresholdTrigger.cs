using EntityFrameworkCore.Triggered;
using Validator.Domain;
using Validator.Domain.Table.ChangeLeader;
using Validator.Infrastructure.Handler.Command.ChangeLeader;
using Validator.Infrastructure.Repository.ChangeLeader;
using Validator.Infrastructure.Service;
using Validator.Infrastructure.Service.ChangeLeader;

namespace Validator.API.Triggers.ChangeLeader;
public class ElectionLocalChangeLeaderThresholdTrigger : IAfterSaveTrigger<LocalVoteChangeLeader>
{
    private readonly IApproverService _approverService;
    private readonly ILocalVoteChangeLeaderService _electionLocalVoteService;
    private readonly ILocalVoteChangeLeaderHistoryRepository _electionVoteLocalCompletedHistoryRepository;

    public ElectionLocalChangeLeaderThresholdTrigger(IApproverService approverService, ILocalVoteChangeLeaderService electionLocalVoteService, ILocalVoteChangeLeaderHistoryRepository electionVoteLocalCompletedHistoryRepository)
    {
        _approverService = approverService;
        _electionLocalVoteService = electionLocalVoteService;
        _electionVoteLocalCompletedHistoryRepository = electionVoteLocalCompletedHistoryRepository;
    }

    public async Task AfterSave(ITriggerContext<LocalVoteChangeLeader> context, CancellationToken cancellationToken)
    {
        var addedItem = context.Entity;
        var isVoteCompleted = await _electionVoteLocalCompletedHistoryRepository.IsVoteCompleted(addedItem.ElectionChangeLeaderId, cancellationToken);
        if (isVoteCompleted)
            return;

        var resultVoteCounter = await _electionLocalVoteService.IsVoteCountGreaterThanThreshold(addedItem.ElectionChangeLeaderId, addedItem.Vote, cancellationToken);
        if (!resultVoteCounter)
            return;

        var voteLocalCompleted = new LocalVoteChangeLeaderHistory()
        {
            ElectionChangeLeaderId = addedItem.ElectionChangeLeaderId,
        };

        var electionNotifyLeaderCompleted = new ElectionChangeLeaderNotifyLeaderCompleted()
        {
            Vote = addedItem.Vote,
            ApproverId = addedItem.ApproverId,
            ElectionChangeLeaderId = addedItem.ElectionChangeLeaderId
        };

        await _electionVoteLocalCompletedHistoryRepository.Add(voteLocalCompleted, cancellationToken);
        await _approverService.SendPostToLeaderApprover(Routes.ElectionNotifyLeaderCompleted, electionNotifyLeaderCompleted, cancellationToken);
    }
}
