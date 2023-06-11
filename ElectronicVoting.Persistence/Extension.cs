using Microsoft.Extensions.DependencyInjection;

namespace ElectronicVoting.Persistence
{
    public static class Extension
    {
        public static IServiceCollection AddMainPersistence(this IServiceCollection services)
        {
            services.AddDbContext<MainDbContext>();
            return services;
        }

        public static IServiceCollection AddValidatorPersistence(this IServiceCollection services)
        {
            services.AddDbContext<ValidatorDbContext>();
            return services;
        }

        public static IServiceCollection AddCommonPersistence(this IServiceCollection services)
        {
            services.AddDbContext<CommonDbContext>();
            return services;
        }
    }
}