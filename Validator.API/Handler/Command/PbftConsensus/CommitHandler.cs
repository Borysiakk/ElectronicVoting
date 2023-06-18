
using ElectronicVoting.Infrastructure.Repository;

using ElectronicVoting.Validator.Infrastructure.Helper;
using MediatR;
using Validator.Domain.Enum;
using Validator.Domain.Handler.Command.Consensu;
using Validator.Domain.Queue.Consensus;
using Validator.Domain.Table;

namespace ElectronicVoting.API.Handler.Command.PbftConsensus;
public class CommitHandler : IRequestHandler<Commit>
{
    private readonly PbftOperationsConsensusRepository _pbftOperationsConsensusRepository;

    public CommitHandler(PbftOperationsConsensusRepository pbftOperationsConsensusRepository)
    {
        _pbftOperationsConsensusRepository = pbftOperationsConsensusRepository;
    }

    public async Task Handle(Commit request, CancellationToken cancellationToken)
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
    }
}
