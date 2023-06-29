﻿using ElectronicVoting.Infrastructure.Repository;
using EntityFrameworkCore.Triggered;
using Newtonsoft.Json;
using Validator.Domain.Enum;
using Validator.Domain.Queue.Consensus;
using Validator.Domain.Table;

namespace Validator.Infrastructure.Triggers;
public class AfterCreateTransactionPending : IAfterSaveTrigger<TransactionPending>
{
    private readonly ISettingRepository _settingRepository;
    private readonly TransactionPendingRepository _transactionPendingRepository;
    private readonly TransactionConfirmedRepository _transactionConfirmedRepository;
    private readonly PbftOperationsConsensusRepository _pbftOperationsConsensusRepository;

    public AfterCreateTransactionPending(ISettingRepository settingRepository, TransactionPendingRepository transactionPendingRepository, TransactionConfirmedRepository transactionConfirmedRepository, PbftOperationsConsensusRepository pbftOperationsConsensusRepository)
    {
        _settingRepository = settingRepository;
        _transactionPendingRepository = transactionPendingRepository;
        _transactionConfirmedRepository = transactionConfirmedRepository;
        _pbftOperationsConsensusRepository = pbftOperationsConsensusRepository;
    }

    public async Task AfterSave(ITriggerContext<TransactionPending> context, CancellationToken cancellationToken)
    {
        var addedItem = context.Entity;

        var setting = await _settingRepository.GetAsync("Validator", "AcceptableValidatorsCount", cancellationToken);

        if (setting == null)
            throw new Exception("Setting not found!");

        var acceptableNumberValidators = Int64.Parse(setting.Value);
        var numberOfApprovedTransactions = _transactionPendingRepository.CountTransactionPendingByIdAndByHash(addedItem.TransactionId, addedItem.Hash);

        if (numberOfApprovedTransactions >= acceptableNumberValidators)
        {
            if (!await _transactionConfirmedRepository.IsExistsTransactionConfirmedById(addedItem.TransactionId))
            {
                var pbftOperation = await _pbftOperationsConsensusRepository.GetByIdAndStatus(addedItem.TransactionId, PbftOperationType.Prepare, cancellationToken);
                if (pbftOperation == null)
                    throw new Exception("Transaction is not exist");

                TransactionConfirmed transactionConfirmed = new TransactionConfirmed()
                {
                    Hash = addedItem.Hash,
                    TransactionId = addedItem.TransactionId,
                    Voice = JsonConvert.DeserializeObject<ItemBodyPrepare>(pbftOperation.Body).Voice,
                };


                ///Dodać wstawianie prze lidera a nie jeżeli wiekszośc zostanie potwierdzona

                await _transactionConfirmedRepository.AddAsync(transactionConfirmed, cancellationToken);
                await _transactionConfirmedRepository.SaveAsync(cancellationToken);
            }
        }
    }
}
