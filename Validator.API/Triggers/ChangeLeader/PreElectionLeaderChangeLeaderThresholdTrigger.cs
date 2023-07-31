using EntityFrameworkCore.Triggered;
using Validator.Domain;
using Validator.Domain.Table.ChangeLeader;
using Validator.Infrastructure.Handler.Command.ChangeLeader;
using Validator.Infrastructure.Repository.ChangeLeader;
using Validator.Infrastructure.Service;
using Validator.Infrastructure.Service.ChangeLeader;

namespace Validator.API.Triggers.ChangeLeader;


public class PreElectionLeaderChangeLeaderThresholdTrigger : IAfterSaveTrigger<PreLeaderVoteChangeLeader>
{
    private readonly IApproverService _approverService;
    private readonly IPreLeaderVoteChangeLeaderService _preElectionLeaderVoteService;
    private readonly IPreLeaderVoteChangeLeaderHistoryRepository _preVoteLeaderChangeLeaderHistoryRepository;

    public PreElectionLeaderChangeLeaderThresholdTrigger(IApproverService approverService, IPreLeaderVoteChangeLeaderService preElectionLeaderVoteService, IPreLeaderVoteChangeLeaderHistoryRepository preVoteLeaderChangeLeaderHistoryRepository)
    {
        _approverService = approverService;
        _preElectionLeaderVoteService = preElectionLeaderVoteService;
        _preVoteLeaderChangeLeaderHistoryRepository = preVoteLeaderChangeLeaderHistoryRepository;
    }

    public async Task AfterSave(ITriggerContext<PreLeaderVoteChangeLeader> context, CancellationToken cancellationToken)
    {
        var addedItem = context.Entity;
        var isVoteCompleted = await _preVoteLeaderChangeLeaderHistoryRepository.IsVoteCompleted(addedItem.PreElectionChangeLeaderId, cancellationToken);

        if (isVoteCompleted)
            return;

        var resultVoteCounter = await _preElectionLeaderVoteService.IsVoteCountGreaterThanThreshold(addedItem.PreElectionChangeLeaderId, addedItem.Decision, cancellationToken);

        if (!resultVoteCounter)
            return;

        var preElectionVoteLeaderCompletedHistory = new PreLeaderVoteChangeLeaderHistory()
        {
            PreElectionChangeLeaderId = addedItem.PreElectionChangeLeaderId
        };

        await _preVoteLeaderChangeLeaderHistoryRepository.Add(preElectionVoteLeaderCompletedHistory,cancellationToken);
        await _approverService.SendPostToLeaderApprover(Routes.ElectionPreparationInitialization, new ElectionPreparationInitChangeLeader(), cancellationToken);
    }
}
