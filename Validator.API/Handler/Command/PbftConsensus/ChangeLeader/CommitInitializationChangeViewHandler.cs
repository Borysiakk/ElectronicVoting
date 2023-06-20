using ElectronicVoting.Infrastructure.Repository;
using ElectronicVoting.Validator.Infrastructure.Helper;
using MediatR;
using Validator.Domain.Enum;
using Validator.Domain.Handler.Command.Consensu;
using Validator.Domain.Models.Queue.Consensus.ChangeView;
using Validator.Domain.Table;

namespace Validator.API.Handler.Command.PbftConsensus.ChangeLeader;
public class CommitInitializationChangeViewHandler : IRequestHandler<CommitInitializationChangeView>
{
    private readonly PbftOperationsConsensusRepository _pbftOperationsConsensusRepository;

    public CommitInitializationChangeViewHandler(PbftOperationsConsensusRepository pbftOperationsConsensusRepository)
    {
        _pbftOperationsConsensusRepository = pbftOperationsConsensusRepository;
    }

    public async Task Handle(CommitInitializationChangeView request, CancellationToken cancellationToken)
    {
        var item = new ItemBodyCommitInitializationChangeView()
        {
            Round = request.Round,
            Decision = request.Decision,
            ApproverId = request.ApproverId,
            TransactionId = request.TransactionId,
        };

        var operations = new PbftOperationConsensus()
        {
            Body = item.SerializeJson(),
            TransactionId = request.TransactionId,
            Status = PbftOperationStatus.NotReady,
            Operations = PbftOperationType.CommitInitializationChangeView,
        };

        await _pbftOperationsConsensusRepository.AddAsync(operations, cancellationToken);
        await _pbftOperationsConsensusRepository.SaveAsync(cancellationToken);
    }
}
