using ElectronicVoting.Domain.Enum;
using ElectronicVoting.Domain.Handler.Command.Consensu;
using ElectronicVoting.Domain.Models.Queue.Consensus;
using ElectronicVoting.Domain.Table;
using ElectronicVoting.Infrastructure.Helper;
using ElectronicVoting.Infrastructure.Repository;
using MediatR;

namespace ElectronicVoting.API.Handler.Command.PbftConsensus
{
    public class PrepareHandler : IRequestHandler<Prepare>
    {
        private readonly PbftOperationsConsensusRepository _pbftOperationsConsensusRepository;

        public PrepareHandler(PbftOperationsConsensusRepository pbftOperationsConsensusRepository)
        {
            _pbftOperationsConsensusRepository = pbftOperationsConsensusRepository;
        }

        public async Task<Unit> Handle(Prepare request, CancellationToken cancellationToken)
        {
            var item = new ItemBodyPrepare() {
                Voice = request.Voice,
                TransactionId = request.TransactionId,
            };
            
            var operations = new PbftOperationConsensus()  {
                Body = item.SerializeJson(),
                TransactionId = request.TransactionId,
                Status = PbftOperationStatus.NotReady,
                Operations = PbftOperationType.Prepare,
            };
            
            await _pbftOperationsConsensusRepository.AddAsync(operations, cancellationToken);
            await _pbftOperationsConsensusRepository.SaveAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
