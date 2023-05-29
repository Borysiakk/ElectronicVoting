﻿using ElectronicVoting.Domain.Enum;
using ElectronicVoting.Domain.Models.Queue.Consensus;
using ElectronicVoting.Domain.Table;
using ElectronicVoting.Infrastructure.Helper;
using ElectronicVoting.Infrastructure.Repository;
using MediatR;

namespace ElectronicVoting.API.Handler.Command.PbftConsensus
{
    public class PrePrepare :IRequest
    {
        public long Voice { get; set; }
    }

    public class PrePrepareHandler : IRequestHandler<PrePrepare>
    {
        private readonly ValidatorRepository _validatorRepository;
        private readonly PbftOperationsConsensusRepository _pbftOperationsConsensusRepository;

        public PrePrepareHandler(PbftOperationsConsensusRepository pbftOperationsConsensusRepository, ValidatorRepository validatorRepository)
        {
            _validatorRepository = validatorRepository;
            _pbftOperationsConsensusRepository = pbftOperationsConsensusRepository;
        }

        public async Task<Unit> Handle(PrePrepare request, CancellationToken cancellationToken)
        {
            var transactionId = Guid.NewGuid().ToString();

            var transaction = new RegisteredTransaction() {
                TransactionId = transactionId
            };

            var item = new ItemBodyPrePrepare() {
                Voice = request.Voice,
                TransactionId = transactionId
            };

            var operations = new PbftOperationConsensus() {
                Body = item.SerializeJson(),
                Status = PbftOperationStatus.NotReady,
                Operations = PbftOperationType.PrePrepare,
            };
            
            foreach (var validator in await _validatorRepository.GetAllAsync(cancellationToken)) {
                var t = await HttpHelper.PostAsync<RegisteredTransaction>(validator.Address, Routes.TransactionRegister, transaction);

                await _pbftOperationsConsensusRepository.AddAsync(operations, cancellationToken);
                await _pbftOperationsConsensusRepository.SaveAsync(cancellationToken);
            }
            
            return Unit.Value;
        }
    }
}
