
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ElectronicVoting.Infrastructure.Repository;
using ElectronicVoting.Infrastructure.Services;
using ElectronicVoting.Domain.Enum;
using System.Data;

namespace ElectronicVoting.Infrastructure.Queue
{
    public class BackgroundPbftOperationsConsensus : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public BackgroundPbftOperationsConsensus(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            {
                var _pbftConsensus = scope.ServiceProvider.GetRequiredService<IPbftConsensusService>();
                var _pbftOperationsConsensusRepository = scope.ServiceProvider.GetRequiredService<PbftOperationsConsensusRepository>();

                while (!cancellationToken.IsCancellationRequested)
                {
                    var transaction = await _pbftOperationsConsensusRepository.CreateTransactionAsync(IsolationLevel.ReadUncommitted, cancellationToken);

                    var itemOperations = await _pbftOperationsConsensusRepository.GetByStatus();

                    foreach (var item in itemOperations)
                        item.Status = PbftOperationStatus.Ready;


                    _pbftOperationsConsensusRepository.UpdateRange(itemOperations);
                    await _pbftOperationsConsensusRepository.SaveAsync(cancellationToken);
                    await transaction.CommitAsync(cancellationToken);

                    foreach (var item in itemOperations) 
                    {
                        switch (item.Operations)
                        {
                            case PbftOperationType.PrePrepare:
                                await _pbftConsensus.PrePrepareAsync(item, cancellationToken);
                                break;
                            case PbftOperationType.Prepare:
                                await _pbftConsensus.PrepareAsync(item, cancellationToken);
                                break;
                            case PbftOperationType.Commit:
                                await _pbftConsensus.CommitAsync(item, cancellationToken);
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    }
                }
            }

            await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
        }
    }
}
