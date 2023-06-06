using ElectronicVoting.Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ElectronicVoting.Infrastructure.Queue
{
    public class BackgroundConfirmedTransactions : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private TransactionRegisterRepository _transactionRegisterRepository;
        private TransactionConfirmedRepository _transactionConfirmedRepository;

        public BackgroundConfirmedTransactions(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }


        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            _transactionRegisterRepository = scope.ServiceProvider.GetRequiredService<TransactionRegisterRepository>();
            _transactionConfirmedRepository = scope.ServiceProvider.GetRequiredService<TransactionConfirmedRepository>();
            while (!cancellationToken.IsCancellationRequested)
            {
                var transactionRegisters = await _transactionRegisterRepository.GetByIsInserted(false, cancellationToken);
                var confirmedTransactions = await _transactionConfirmedRepository.GetByIsInserted(false, cancellationToken);

                foreach (var transaction in transactionRegisters)
                {
                    var confirmedTransaction = confirmedTransactions.FirstOrDefault(a => a.TransactionId == transaction.TransactionId);
                    if (confirmedTransaction == null) break;

                    transaction.IsInserted = true;
                    confirmedTransaction.IsInserted = true;

                    _transactionRegisterRepository.Update(transaction);
                    _transactionConfirmedRepository.Update(confirmedTransaction);

                    await _transactionRegisterRepository.SaveAsync(cancellationToken);
                    await _transactionConfirmedRepository.SaveAsync(cancellationToken);

                    Console.WriteLine("Głos został wstawiony");
                }
            }

            await Task.Delay(TimeSpan.FromSeconds(2), cancellationToken);
        }
    }
}
