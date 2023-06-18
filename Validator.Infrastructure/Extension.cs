using ElectronicVoting.Infrastructure.Queue;
using ElectronicVoting.Infrastructure.Repository;
using ElectronicVoting.Infrastructure.Services;
using ElectronicVoting.Persistence;
using ElectronicVoting.Validator.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Validator.Infrastructure.Repository;
using Validator.Infrastructure.Triggers;

namespace ElectronicVoting.Infrastructure
{
    public static class Extension
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection service)
        {
            service.AddScoped<IBlockService, BlockService>();
            service.AddScoped<ITransactionService, TransactionService>();
            service.AddScoped<IBlochchainService, BlochchainService>();
            service.AddScoped<ISettingRepository,SettingRepository>();
            service.AddScoped<ApproverRepository>();
            service.AddScoped<TransactionRepository>();
            service.AddScoped<TransactionPendingRepository>();
            service.AddScoped<TransactionConfirmedRepository>();
            service.AddScoped<TransactionRegisterRepository>();
            service.AddScoped<PbftOperationsConsensusRepository>();
            service.AddScoped<ChangeViewTransactionRepository>();
            service.AddScoped<InitializationChangeViewTransactionRepository>();
            service.AddScoped<IPbftConsensusService, PbftConsesusService>();
            service.AddScoped<IProofOfKnowledgeService, ProofOfKnowledgeService>();
            service.AddTransient(typeof(IPipelineBehavior<,>), typeof(DbContextTransactionPipelineBehavior<,>));
            service.AddHostedService<BackgroundPbftOperationsConsensus>();
            service.AddHostedService<BackgroundConfirmedTransactions>();

            service.AddDbContext<ValidatorDbContext>( option =>
            {
                option.UseTriggers(triggers =>
                {
                    triggers.AddTrigger<AfterCreateTransactionPending>();
                    triggers.AddTrigger<AfterCreateInitializationChangeViewTransaction>();
                });
            });

            return service;
        }
    }
}