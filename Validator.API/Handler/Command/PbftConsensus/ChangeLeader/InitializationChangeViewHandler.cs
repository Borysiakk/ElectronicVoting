using ElectronicVoting.Infrastructure.Repository;
using ElectronicVoting.Validator.Infrastructure.Helper;
using MediatR;
using Validator.Domain.Enum;
using Validator.Domain.Handler.Command.Consensu.ChangeLeader;
using Validator.Domain.Models.Queue.Consensus.ChangeView;
using Validator.Domain.Table;

namespace Validator.API.Handler.Command.PbftConsensus.ChangeLeader;

public class InitializationChangeViewHandler : IRequestHandler<InitializationChangeView>
{
    private readonly PbftOperationsConsensusRepository _pbftOperationsConsensusRepository;

    public InitializationChangeViewHandler(PbftOperationsConsensusRepository pbftOperationsConsensusRepository)
    {
        _pbftOperationsConsensusRepository = pbftOperationsConsensusRepository;
    }

    public async Task Handle(InitializationChangeView request, CancellationToken cancellationToken)
    {
        //Dodać czy na pewno lider musi zostać zmieniony, np jeżeli nie przepuści poprawnie zweryfikowanych blków lub jeżeli minie wyznaczony czas bycia liderem

        var item = new ItemBodyInitializationChangeView()
        {
            Decision = true,
            Round = request.Round,
            TransactionId = request.TransactionId,
        };

        var operations = new PbftOperationConsensus()
        {
            Body = item.SerializeJson(),
            TransactionId = request.TransactionId,
            Status = PbftOperationStatus.NotReady,
            Operations = PbftOperationType.InitializationChangeView,
        };

        await _pbftOperationsConsensusRepository.AddAsync(operations, cancellationToken);
        await _pbftOperationsConsensusRepository.SaveAsync(cancellationToken);
    }
}
