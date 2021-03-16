
using ElectronicVoting.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ElectronicVoting.Persistence
{
    public static class DependencyInjection
    {
        public static string DbConnection = @"Server=(localdb)\ElectronicVoting.Validator;Database=aspnet-63bc9b9d-9d6a-45d4-8429-2a2761773502;Trusted_Connection=True;MultipleActiveResultSets=true";
        
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 1;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
            });
            
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(DbConnection));
            return services;
        }
    }
}