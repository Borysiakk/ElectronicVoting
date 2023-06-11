using ElectronicVoting.Common.Helper;
using ElectronicVoting.Infrastructure.Repository;
using ElectronicVoting.Validator.Domain;
using ElectronicVoting.Validator.Domain.Enum;
using ElectronicVoting.Validator.Domain.Handler.Command.Consensu;
using ElectronicVoting.Validator.Domain.Queue.Consensus;
using ElectronicVoting.Validator.Domain.Table;
using ElectronicVoting.Validator.Infrastructure.Helper;
using MediatR;

namespace ElectronicVoting.API.Handler.Command.PbftConsensus;
public class PrePrepareHandler : IRequestHandler<PrePrepare>
{
    private readonly ApproverRepository _approverRepository;
    private readonly PbftOperationsConsensusRepository _pbftOperationsConsensusRepository;

    public PrePrepareHandler(PbftOperationsConsensusRepository pbftOperationsConsensusRepository, ApproverRepository approverRepository)
    {
        _approverRepository = approverRepository;
        _pbftOperationsConsensusRepository = pbftOperationsConsensusRepository;
    }

    public async Task Handle(PrePrepare request, CancellationToken cancellationToken)
    {
        var transactionId = Guid.NewGuid().ToString();

        var transaction = new TransactionRegister() {
            TransactionId = transactionId
        };

        var item = new ItemBodyPrePrepare() {
            Voice = request.Voice,
            TransactionId = transactionId
        };
        
        foreach (var validator in await _approverRepository.GetAllAsync(cancellationToken)) 
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
    }
}
