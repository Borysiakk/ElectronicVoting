using Microsoft.Extensions.DependencyInjection;

namespace Validator.Infrastructure.RestSharp;

public static class Extensions
{
    public static void AddRestClient(this IServiceCollection services)
    {
        services.AddScoped<IRestCommunicator, RestCommunicator>();
    }

    public static void ConfigureRefitClient<T>(IServiceCollection services, string serviceName)
    {

    }
}
