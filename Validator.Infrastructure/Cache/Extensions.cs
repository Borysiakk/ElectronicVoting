using Microsoft.Extensions.DependencyInjection;

namespace Validator.Infrastructure.Cache;

public static class Extensions
{
    public static void AddCache(this IServiceCollection services)
    {
        services.AddMemoryCache();
        services.AddScoped<ICacheService, CacheService>();
    }
}
