using ElectronicVoting.Infrastructure.Repository;
using ElectronicVoting.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Validator.Domain.Table.Blockchain;

namespace ElectronicVoting.Infrastructure.Queue;

public class BackgroundConfirmedTransactions : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    private Block _block;
    private IBlockService _blockService;
    private IBlochchainService _blockChainService;
    private ITransactionService _transactionService;
    private TransactionRegisterRepository _transactionRegisterRepository;
    private TransactionConfirmedRepository _transactionConfirmedRepository;

    public BackgroundConfirmedTransactions(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;

        using (var scope = _serviceProvider.CreateScope())
        {
            _blockService = scope.ServiceProvider.GetRequiredService<IBlockService>();
            _block = _blockService.Create();
        }
    }


    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        _blockService = scope.ServiceProvider.GetRequiredService<IBlockService>();
        _blockChainService = scope.ServiceProvider.GetRequiredService<IBlochchainService>();
        _transactionService = scope.ServiceProvider.GetRequiredService<ITransactionService>();

        _transactionRegisterRepository = scope.ServiceProvider.GetRequiredService<TransactionRegisterRepository>();
        _transactionConfirmedRepository = scope.ServiceProvider.GetRequiredService<TransactionConfirmedRepository>();
        
        while (!cancellationToken.IsCancellationRequested)
        {
            var transactionRegisters = await _transactionRegisterRepository.GetByIsInserted(false, cancellationToken);
            var confirmedTransactions = await _transactionConfirmedRepository.GetByIsInserted(false, cancellationToken);

            foreach (var transactionR in transactionRegisters)
            {
                var confirmedTransaction = confirmedTransactions.FirstOrDefault(a => a.TransactionId == transactionR.TransactionId);
                if (confirmedTransaction == null) 
                    break;

                transactionR.IsInserted = true;
                confirmedTransaction.IsInserted = true;

                _transactionRegisterRepository.Update(transactionR);
                _transactionConfirmedRepository.Update(confirmedTransaction);

                await _transactionRegisterRepository.SaveAsync(cancellationToken);
                await _transactionConfirmedRepository.SaveAsync(cancellationToken);

                var transaction = _transactionService.Create(confirmedTransaction.Voice);

                _blockService.AddTransaction(_block, transaction);
            }

            if (_block.Transactions.Count > 0)
            {
                await _blockChainService.SaveBlock(_block, cancellationToken);
                _block = _blockService.Create();
            }

            await Task.Delay(TimeSpan.FromSeconds(5), cancellationToken);
        }
    }

}
