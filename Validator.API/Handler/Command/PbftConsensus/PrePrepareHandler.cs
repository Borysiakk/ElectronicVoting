using MediatR;
using Validator.Domain;
using Validator.Domain.Enum;
using Validator.Domain.Handler.Command.Consensu;
using Validator.Domain.Queue.Consensus;
using Validator.Domain.Table;
using ElectronicVoting.Common.Helper;
using ElectronicVoting.Infrastructure.Repository;
using ElectronicVoting.Validator.Infrastructure.Helper;
using Validator.Infrastructure.Repository;

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
