using ElectronicVoting.Domain.Enum;
using ElectronicVoting.Domain.Handler.Command.Consensu;
using ElectronicVoting.Domain.Models.Queue.Consensus;
using ElectronicVoting.Domain.Table;
using ElectronicVoting.Infrastructure.Helper;
using ElectronicVoting.Infrastructure.Repository;
using MediatR;

namespace ElectronicVoting.API.Handler.Command.PbftConsensus
{
    public class CommitHandler : IRequestHandler<Commit>
    {
        private readonly PbftOperationsConsensusRepository _pbftOperationsConsensusRepository;

        public CommitHandler(PbftOperationsConsensusRepository pbftOperationsConsensusRepository)
        {
            _pbftOperationsConsensusRepository = pbftOperationsConsensusRepository;
        }

        public async Task<Unit> Handle(Commit request, CancellationToken cancellationToken)
        {
            var item = new ItemBodyCommit() {
                Hash = request.Hash,
                TransactionId = request.TransactionId,
            };

            var operation = new PbftOperationConsensus() {
                Body = item.SerializeJson(),
                Status = PbftOperationStatus.NotReady,
                Operations = PbftOperationType.Commit,
            };

            await _pbftOperationsConsensusRepository.AddAsync(operation, cancellationToken);
            await _pbftOperationsConsensusRepository.SaveAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
