using EntityFrameworkCore.Triggered;
using Validator.API.Handler.Command.ChangeLeader;
using Validator.Domain;
using Validator.Domain.Table.ChangeLeader;
using Validator.Infrastructure.Repository.ChangeLeader;
using Validator.Infrastructure.Service;
using Validator.Infrastructure.Service.ChangeLeader;

namespace Validator.API.Triggers;
public class PreElectionLocalVoteThresholdTrigger : IAfterSaveTrigger<PreLocalVoteChangeLeader>
{
    private readonly IApproverService _approverService;
    private readonly IPreLocalVoteChangeLeaderService _preElectionLocalVoteService;
    private readonly IPreLocalVoteChangeLeaderHistoryRepository _preLocalVoteChangeLeaderHistoryRepository;

    public PreElectionLocalVoteThresholdTrigger(IApproverService approverService, IPreLocalVoteChangeLeaderService preElectionLocalVoteService, IPreLocalVoteChangeLeaderHistoryRepository preLocalVoteChangeLeaderHistoryRepository)
    {
        _approverService = approverService;
        _preElectionLocalVoteService = preElectionLocalVoteService;
        _preLocalVoteChangeLeaderHistoryRepository = preLocalVoteChangeLeaderHistoryRepository;
    }

    public async Task AfterSave(ITriggerContext<PreLocalVoteChangeLeader> context, CancellationToken cancellationToken)
    {
        var addedItem = context.Entity;
        var isVoteCompleted = await _preLocalVoteChangeLeaderHistoryRepository.IsVoteCompleted(addedItem.PreElectionChangeLeaderId, cancellationToken);
        
        if (!isVoteCompleted)
        {

            var resultVoteCounter = await _preElectionLocalVoteService.IsVoteCountGreaterThanThreshold(addedItem.PreElectionChangeLeaderId, addedItem.Decision, cancellationToken);
            if (resultVoteCounter)
            {
                var voteLocalCompleted = new PreLocalVoteChangeLeaderHistory()
                {
                    PreElectionChangeLeaderId = addedItem.PreElectionChangeLeaderId,
                };

                var preElectionNotifyLeaderCompleted = new PreElectionChangeLeaderNotifyLeaderCompleted()
                {
                    Decision = addedItem.Decision,
                    ApproverId = addedItem.ApproverId,
                    PreElectionChangeLeaderId = addedItem.PreElectionChangeLeaderId
                };

                await _preLocalVoteChangeLeaderHistoryRepository.Add(voteLocalCompleted, cancellationToken);
                await _approverService.SendPostToLeaderApprover(Routes.PreElectionNotifyLeaderCompleted, preElectionNotifyLeaderCompleted, cancellationToken);
            }
        }
    }
}

