using MediatR;
using ElectronicVoting.Infrastructure.Repository;
using EntityFrameworkCore.Triggered;
using Validator.Domain.Handler.Command.Consensu.ChangeLeader;
using Validator.Infrastructure.Repository;

namespace Validator.Infrastructure.Triggers
{
    public class AfterCreatePreElectionVote : IAfterSaveTrigger<PreElectionVoteRecord>
    {
        private readonly IMediator _mediator;
        private readonly ISettingRepository _settingRepository;
        private readonly ApproverRepository _approverRepository;
        private readonly PreElectionVoteRepository _preElectionVoteRepository;

        public AfterCreatePreElectionVote(ISettingRepository settingRepository, ApproverRepository approverRepository, PreElectionVoteRepository preElectionVoteRepository, IMediator mediator)
        {
            _mediator = mediator;
            _settingRepository = settingRepository;
            _approverRepository = approverRepository;
            _preElectionVoteRepository = preElectionVoteRepository;
        }

        public async Task AfterSave(ITriggerContext<PreElectionVoteRecord> context, CancellationToken cancellationToken)
        {
            var addedItem = context.Entity;
            var acceptableNumberValidators = 0;
            var setting = await _settingRepository.GetAsync("Validator", "AcceptableValidatorsCount", cancellationToken);

            var approvedChangeLeader = await _preElectionVoteRepository.GetCountByTransactionIdAndDecision(addedItem.TransactionId, cancellationToken);

            if (approvedChangeLeader >= acceptableNumberValidators)
            {
                var electionPreparation = new ElectionPreparation()
                {
                    TransactionId = addedItem.TransactionId
                };
                await _mediator.Send(electionPreparation, cancellationToken);
            }

        }
    }
}
