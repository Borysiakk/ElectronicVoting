using MediatR;
using ElectronicVoting.Infrastructure.Repository;
using ElectronicVoting.Validator.Infrastructure.Helper;
using Validator.Domain.Handler.Command.Consensu;
using Validator.Domain.Table;
using Validator.Domain.Enum;
using Validator.Domain.Models.Queue.Consensus.ChangeView;

namespace Validator.API.Handler.Command.PbftConsensus;

public class PreInitializationChangeViewHandler : IRequestHandler<PreInitializationChangeView>
{
    private readonly PbftOperationsConsensusRepository _pbftOperationsConsensusRepository;

    public PreInitializationChangeViewHandler(PbftOperationsConsensusRepository pbftOperationsConsensusRepository)
    {
        _pbftOperationsConsensusRepository = pbftOperationsConsensusRepository;
    }

    public async Task Handle(PreInitializationChangeView request, CancellationToken cancellationToken)
    {
        var item = new ItemBodyPreInitializationChangeView()
        {
            Round = request.Round,
            TransactionId = request.TransactionId,
        };

        var operations = new PbftOperationConsensus()
        {
            Body = item.SerializeJson(),
            TransactionId = request.TransactionId,
            Status = PbftOperationStatus.NotReady,
            Operations = PbftOperationType.PreInitializationChangeView,
        };

        await _pbftOperationsConsensusRepository.AddAsync(operations, cancellationToken);
        await _pbftOperationsConsensusRepository.SaveAsync(cancellationToken);
    }
}
