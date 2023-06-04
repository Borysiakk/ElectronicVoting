
using ElectronicVoting.Infrastructure.PipelineBehavior;
using ElectronicVoting.Infrastructure.Repository;
using ElectronicVoting.Infrastructure.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ElectronicVoting.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection service)
        {
            service.AddScoped<ValidatorRepository>();
            service.AddScoped<TransactionRepository>();
            service.AddScoped<TransactionPendingRepository>();
            service.AddScoped<PbftOperationsConsensusRepository>();
            service.AddScoped<IPbftConsensusService, PbftConsesusService>();
            service.AddScoped<IProofOfKnowledgeService, ProofOfKnowledgeService>();
            service.AddTransient(typeof(IPipelineBehavior<,>), typeof(DbContextTransactionPipelineBehavior<,>));
            //service.AddHostedService<BackgroundPbftOperationsConsensus>();

            return service;
        }
    }
}