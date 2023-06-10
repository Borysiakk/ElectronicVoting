using ElectronicVoting.Common;
using ElectronicVoting.Domain.Enum;
using ElectronicVoting.Domain.Handler.Command.Consensu;
using ElectronicVoting.Domain.Models.Queue.Consensus;
using ElectronicVoting.Domain.Table;
using ElectronicVoting.Infrastructure.Helper;
using ElectronicVoting.Infrastructure.Repository;
using MediatR;

namespace ElectronicVoting.API.Handler.Command.PbftConsensus
{
    public class PrePrepareHandler : IRequestHandler<PrePrepare>
    {
        private readonly ValidatorRepository _validatorRepository;
        private readonly PbftOperationsConsensusRepository _pbftOperationsConsensusRepository;

        public PrePrepareHandler(PbftOperationsConsensusRepository pbftOperationsConsensusRepository, ValidatorRepository validatorRepository)
        {
            _validatorRepository = validatorRepository;
            _pbftOperationsConsensusRepository = pbftOperationsConsensusRepository;
        }

        public async Task<Unit> Handle(PrePrepare request, CancellationToken cancellationToken)
        {
            var transactionId = Guid.NewGuid().ToString();

            var transaction = new TransactionRegister() {
                TransactionId = transactionId
            };

            var item = new ItemBodyPrePrepare() {
                Voice = request.Voice,
                TransactionId = transactionId
            };
            
            foreach (var validator in await _validatorRepository.GetAllAsync(cancellationToken)) 
            {
                var operations = new PbftOperationConsensus() {
                    Body = item.SerializeJson(),
                    Status = PbftOperationStatus.NotReady,
                    Operations = PbftOperationType.PrePrepare,
                };

                HttpHelper.PostAsync<TransactionRegister>(validator.Address, Routes.TransactionRegister, transaction, cancellationToken);

                await _pbftOperationsConsensusRepository.AddAsync(operations, cancellationToken);
                await _pbftOperationsConsensusRepository.SaveAsync(cancellationToken);
            }
            
            return Unit.Value;
        }
    }
}
