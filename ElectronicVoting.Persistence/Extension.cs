using Microsoft.Extensions.DependencyInjection;

namespace ElectronicVoting.Persistence
{
    public static class Extension
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            services.AddDbContext<MainDbContext>();
            services.AddDbContext<ApplicationDbContext>();
            return services;
        }
    }
}