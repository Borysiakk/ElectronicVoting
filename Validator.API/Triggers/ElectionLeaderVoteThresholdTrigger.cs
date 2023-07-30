using EntityFrameworkCore.Triggered;
using Validator.Infrastructure.Handler.Command.ChangeLeader;
using Validator.Domain;
using Validator.Domain.Table.ChangeLeader;
using Validator.Infrastructure.Repository.ChangeLeader;
using Validator.Infrastructure.Service;
using Validator.Infrastructure.Service.ChangeLeader;

namespace Validator.API.Triggers
{
    public class ElectionLeaderVoteThresholdTrigger : IAfterSaveTrigger<LeaderVoteChangeLeader>
    {
        private readonly IApproverService _approverService;
        private readonly ILeaderVoteChangeLeaderService _leaderVoteChangeLeaderService;
        private readonly ILeaderVoteChangeLeaderHistoryRepository _leaderVoteChangeLeaderHistory;

        public ElectionLeaderVoteThresholdTrigger(IApproverService approverService, ILeaderVoteChangeLeaderService leaderVoteChangeLeaderService, ILeaderVoteChangeLeaderHistoryRepository leaderVoteChangeLeaderHistory)
        {
            _approverService = approverService;
            _leaderVoteChangeLeaderService = leaderVoteChangeLeaderService;
            _leaderVoteChangeLeaderHistory = leaderVoteChangeLeaderHistory;
        }

        public async Task AfterSave(ITriggerContext<LeaderVoteChangeLeader> context, CancellationToken cancellationToken)
        {
            var addedItem = context.Entity;
            var isVoteCompleted = await _leaderVoteChangeLeaderHistory.IsVoteCompleted(addedItem.ElectionChangeLeaderId, cancellationToken);

            if (!isVoteCompleted)
            {
                var resultVoteCounter = await _leaderVoteChangeLeaderService.IsVoteCountGreaterThanThreshold(addedItem.ElectionChangeLeaderId, addedItem.Vote, cancellationToken);

                if (resultVoteCounter)
                {
                    bool includeSender = true;

                    var electionVoteChangeLeaderCompletedHistory = new LeaderVoteChangeLeaderHistory()
                    {
                        ElectionChangeLeaderId = addedItem.ElectionChangeLeaderId,
                    };

                    var electionSetNewLeader = new SetNewLeader()
                    {
                        LeaderApproverId = addedItem.Vote,
                        ElectionChangeLeaderId = addedItem.ElectionChangeLeaderId
                    };

                    await _leaderVoteChangeLeaderHistory.Add(electionVoteChangeLeaderCompletedHistory, cancellationToken);
                    await _approverService.SendPostToApprovers(Routes.ElectionSetNewLeader, electionSetNewLeader, includeSender, cancellationToken);
                } 
            }
        }
    }
}
