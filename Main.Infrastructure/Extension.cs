using Main.Domain.Table;
using ElectronicVoting.Common.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Main.Infrastructure;

public static class Extension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection service)
    {
        service.AddScoped<Repository<Token>>();
        return service;
    }
}
