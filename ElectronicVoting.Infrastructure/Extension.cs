using ElectronicVoting.Infrastructure.Queue;
using ElectronicVoting.Infrastructure.Repository;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ElectronicVoting.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection service)
        {

            service.AddScoped<ValidatorRepository>();
            service.AddScoped<TransactionRepository>();
            service.AddScoped<PbftOperationsConsensusRepository>();

            service.AddHostedService<BackgroundPbftOperationsConsensus>();

            return service;
        }
    }
}