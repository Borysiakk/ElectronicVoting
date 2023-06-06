
using ElectronicVoting.Infrastructure.PipelineBehavior;
using ElectronicVoting.Infrastructure.Queue;
using ElectronicVoting.Infrastructure.Repository;
using ElectronicVoting.Infrastructure.Services;
using ElectronicVoting.Infrastructure.Triggers;
using ElectronicVoting.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ElectronicVoting.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection service)
        {
            service.AddScoped<ISettingRepository,SettingRepository>();
            service.AddScoped<ValidatorRepository>();
            service.AddScoped<TransactionRepository>();
            service.AddScoped<TransactionPendingRepository>();
            service.AddScoped<TransactionConfirmedRepository>();
            service.AddScoped<TransactionRegisterRepository>();
            service.AddScoped<PbftOperationsConsensusRepository>();
            service.AddScoped<IPbftConsensusService, PbftConsesusService>();
            service.AddScoped<IProofOfKnowledgeService, ProofOfKnowledgeService>();
            service.AddTransient(typeof(IPipelineBehavior<,>), typeof(DbContextTransactionPipelineBehavior<,>));
            service.AddHostedService<BackgroundPbftOperationsConsensus>();
            service.AddHostedService<BackgroundConfirmedTransactions>();



            service.AddDbContext<MainDbContext>();
            service.AddDbContext<ApplicationDbContext>( option =>
            {
                option.UseTriggers(triggers =>
                {
                    triggers.AddTrigger<AfterCreateTransactionPending>();
                });
            });

            return service;
        }
    }
}