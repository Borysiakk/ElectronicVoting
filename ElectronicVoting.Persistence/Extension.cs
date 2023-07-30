using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ElectronicVoting.Persistence
{
    public static class Extension
    {
        public static IServiceCollection AddValidatorPersistence(this IServiceCollection services, Action<DbContextOptionsBuilder>? optionsAction )
        {
            services.AddDbContext<ValidatorDbContext>(optionsAction);
            return services;
        }
    }
}