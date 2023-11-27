using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Main.Infrastructure.EntityFramework;

public static class Extensions
{
    public static IServiceCollection AddEntityFramework(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddDbContext<MainDbContext>(option => option.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        return service;
    }
}
