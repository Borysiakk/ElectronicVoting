﻿using ElectronicVoting.Infrastructure.Repository;
using ElectronicVoting.Validator.Infrastructure.Helper;
using MediatR;
using Validator.Domain.Enum;
using Validator.Domain.Handler.Command.Consensu;
using Validator.Domain.Queue.Consensus;
using Validator.Domain.Table;

namespace ElectronicVoting.API.Handler.Command.PbftConsensus;
public class PrepareHandler : IRequestHandler<Prepare>
{
    private readonly PbftOperationsConsensusRepository _pbftOperationsConsensusRepository;

    public PrepareHandler(PbftOperationsConsensusRepository pbftOperationsConsensusRepository)
    {
        _pbftOperationsConsensusRepository = pbftOperationsConsensusRepository;
    }

    public async Task Handle(Prepare request, CancellationToken cancellationToken)
    {
        var item = new ItemBodyPrepare()
        {
            Voice = request.Voice,
            TransactionId = request.TransactionId,
        };

        var operations = new PbftOperationConsensus()
        {
            Body = item.SerializeJson(),
            TransactionId = request.TransactionId,
            Status = PbftOperationStatus.NotReady,
            Operations = PbftOperationType.Prepare,
        };

        await _pbftOperationsConsensusRepository.AddAsync(operations, cancellationToken);
        await _pbftOperationsConsensusRepository.SaveAsync(cancellationToken);
    }
}
