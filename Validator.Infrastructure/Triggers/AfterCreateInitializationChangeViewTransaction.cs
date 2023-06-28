using ElectronicVoting.Infrastructure.Repository;
using EntityFrameworkCore.Triggered;
using Validator.Domain.Enum;
using Validator.Domain.Table;
using Validator.Domain.Table.ChangeView;
using Validator.Infrastructure.Repository;

namespace Validator.Infrastructure.Triggers
{
    public class AfterCreateInitializationChangeViewTransaction : IAfterSaveTrigger<InitializationChangeViewTransaction>
    {
        private readonly ISettingRepository _settingRepository;
        private readonly ApproverRepository _approverRepository;
        private readonly PbftOperationsConsensusRepository _pbftOperationsConsensusRepository;
        private readonly InitializationChangeViewTransactionRepository _initializationChangeViewTransactionRepository;

        public AfterCreateInitializationChangeViewTransaction(ISettingRepository settingRepository, ApproverRepository approverRepository, PbftOperationsConsensusRepository pbftOperationsConsensusRepository, InitializationChangeViewTransactionRepository initializationChangeViewTransactionRepository)
        {
            _settingRepository = settingRepository;
            _approverRepository = approverRepository;
            _pbftOperationsConsensusRepository = pbftOperationsConsensusRepository;
            _initializationChangeViewTransactionRepository = initializationChangeViewTransactionRepository;
        }

        public async Task AfterSave(ITriggerContext<InitializationChangeViewTransaction> context, CancellationToken cancellationToken)
        {
            var addedItem = context.Entity;
            Int64 acceptableNumberValidators = 0;
            var setting = await _settingRepository.GetAsync("Validator", "AcceptableValidatorsCount", cancellationToken);

            if (setting == null)
            {
                var validators = _approverRepository.GetAll();
                acceptableNumberValidators = validators.Count() / 2;
            }

            var approvedChangeLeader = await _initializationChangeViewTransactionRepository.GetCountByTransactionIdAndDecision(addedItem.TransactionId, cancellationToken);

            if(approvedChangeLeader >= acceptableNumberValidators)
            {
                var operations = new PbftOperationConsensus()
                {
                    Body = string.Empty,
                    TransactionId = addedItem.TransactionId,
                    Status = PbftOperationStatus.NotReady,
                    Operations = PbftOperationType.ChangeView,
                };

                await _pbftOperationsConsensusRepository.AddAsync(operations, cancellationToken);
                await _pbftOperationsConsensusRepository.SaveAsync(cancellationToken);
            }
        }
    }
}
