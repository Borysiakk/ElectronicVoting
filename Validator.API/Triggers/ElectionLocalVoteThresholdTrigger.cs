using EntityFrameworkCore.Triggered;
using Validator.API.Handler.Command.ChangeLeader;
using Validator.Domain;
using Validator.Domain.Table.ChangeLeader;
using Validator.Infrastructure.Repository.ChangeLeader;
using Validator.Infrastructure.Service;
using Validator.Infrastructure.Service.ChangeLeader;

namespace Validator.API.Triggers;

public class ElectionLocalVoteThresholdTrigger : IAfterSaveTrigger<LocalVoteChangeLeader>
{

    private readonly IApproverService _approverService;
    private readonly ILocalVoteChangeLeaderService _electionLocalVoteService;
    private readonly ILocalVoteChangeLeaderHistoryRepository _electionVoteLocalCompletedHistoryRepository;

    public ElectionLocalVoteThresholdTrigger(IApproverService approverService, ILocalVoteChangeLeaderService electionLocalVoteService, ILocalVoteChangeLeaderHistoryRepository electionVoteLocalCompletedHistoryRepository)
    {
        _approverService = approverService;
        _electionLocalVoteService = electionLocalVoteService;
        _electionVoteLocalCompletedHistoryRepository = electionVoteLocalCompletedHistoryRepository;
    }

    public async Task AfterSave(ITriggerContext<LocalVoteChangeLeader> context, CancellationToken cancellationToken)
    {
        var addedItem = context.Entity;
        var isVoteCompleted = await _electionVoteLocalCompletedHistoryRepository.IsVoteCompleted(addedItem.ElectionChangeLeaderId, cancellationToken);

        if(!isVoteCompleted)
        {
            var resultVoteCounter = await _electionLocalVoteService.IsVoteCountGreaterThanThreshold(addedItem.ElectionChangeLeaderId, addedItem.Vote, cancellationToken);
            if (resultVoteCounter)
            {
                var voteLocalCompleted = new LocalVoteChangeLeaderHistory()
                {
                    ElectionChangeLeaderId = addedItem.ElectionChangeLeaderId,
                };

                var electionNotifyLeaderCompleted = new ElectionNotifyLeaderCompleted()
                {
                    Vote = addedItem.Vote,
                    ApproverId = addedItem.ApproverId,
                    ElectionChangeLeaderId = addedItem.ElectionChangeLeaderId
                };

                await _electionVoteLocalCompletedHistoryRepository.Add(voteLocalCompleted, cancellationToken);
                await _approverService.SendPostToLeaderApprover(Routes.ElectionNotifyLeaderCompleted, electionNotifyLeaderCompleted, cancellationToken);
            }
        }
    }
}
