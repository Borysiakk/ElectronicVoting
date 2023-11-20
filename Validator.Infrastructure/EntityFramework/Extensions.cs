using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Validator.Infrastructure.EntityFramework
{
    public static class Extensions
    {
        public static IServiceCollection AddEntityFramework(this IServiceCollection service)
        {
            var connectionString = Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING");
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new Exception("Error while reading the connection string from the database.");

            service.AddDbContext<ElectionDatabaseContext>(option => option.UseSqlServer(connectionString);

            return service;
        }
    }
}
