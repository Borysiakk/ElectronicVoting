using ElectronicVoting.Infrastructure.Repository;
using ElectronicVoting.Validator.Domain.Enum;
using ElectronicVoting.Validator.Domain.Table;
using ElectronicVoting.Validator.Infrastructure.Helper;
using MediatR;
using Validator.Domain.Handler.Command.Consensu;
using Validator.Domain.Models.Queue.Consensus;

namespace Validator.API.Handler.Command.PbftConsensus
{
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
}
