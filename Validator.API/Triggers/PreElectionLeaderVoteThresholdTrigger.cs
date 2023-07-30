using EntityFrameworkCore.Triggered;
using Validator.API.Handler.Command.ChangeLeader;
using Validator.Domain;
using Validator.Domain.Table.ChangeLeader;
using Validator.Infrastructure.Repository.ChangeLeader;
using Validator.Infrastructure.Service;
using Validator.Infrastructure.Service.ChangeLeader;

namespace Validator.API.Triggers
{
    public class PreLeaderVoteThresholdTrigger : IAfterSaveTrigger<PreLeaderVoteChangeLeader>
    {
        private readonly IApproverService _approverService;
        private readonly IPreLeaderVoteChangeLeaderService _preElectionLeaderVoteService;
        private readonly IPreLeaderVoteChangeLeaderHistoryRepository _preVoteLeaderChangeLeaderHistoryRepository;

        public PreLeaderVoteThresholdTrigger(IApproverService approverService, IPreLeaderVoteChangeLeaderService preElectionLeaderVoteService, IPreLeaderVoteChangeLeaderHistoryRepository preVoteLeaderChangeLeaderHistoryRepository)
        {
            _approverService = approverService;
            _preElectionLeaderVoteService = preElectionLeaderVoteService;
            _preVoteLeaderChangeLeaderHistoryRepository = preVoteLeaderChangeLeaderHistoryRepository;
        }

        public async Task AfterSave(ITriggerContext<PreLeaderVoteChangeLeader> context, CancellationToken cancellationToken)
        {
            var addedItem = context.Entity;
            var isVoteCompleted = await _preVoteLeaderChangeLeaderHistoryRepository.IsVoteCompleted(addedItem.PreElectionChangeLeaderId, cancellationToken);
            
            if (!isVoteCompleted)
            {
                var resultVoteCounter = await _preElectionLeaderVoteService.IsVoteCountGreaterThanThreshold(addedItem.PreElectionChangeLeaderId, addedItem.Decision, cancellationToken);

                if (resultVoteCounter)
                {
                    var preElectionVoteLeaderCompletedHistory = new PreLeaderVoteChangeLeaderHistory()
                    {
                        PreElectionChangeLeaderId = addedItem.PreElectionChangeLeaderId
                    };

                    var electionPreparationInitialization = new ElectionPreparationInitialization();
                    await _preVoteLeaderChangeLeaderHistoryRepository.Add(preElectionVoteLeaderCompletedHistory,cancellationToken);
                    await _approverService.SendPostToLeaderApprover(Routes.ElectionPreparationInitialization, electionPreparationInitialization, cancellationToken);
                }
            }
        }
    }
}
