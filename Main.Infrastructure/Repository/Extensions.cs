using Microsoft.Extensions.DependencyInjection;

namespace Main.Infrastructure.Repository;

public static class Extensions
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICandidateRepository, CandidateRepository>();
    }
}
