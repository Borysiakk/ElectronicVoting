using Hangfire;
using Hangfire.Redis.StackExchange;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Validator.Infrastructure.Hangfire.Handler;

namespace Validator.Infrastructure.Hangfire;

public static class Extensions
{
    public static void AddHangfire(this IServiceCollection service)
    {
        //ervice.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

        string redisConnectionStrings = Environment.GetEnvironmentVariable("REDIS_URL");
        service.AddHangfire(config => config.UseRedisStorage(redisConnectionStrings, new RedisStorageOptions { }));

        service.AddHangfireServer();
    }


    public static void ConfigureHangFire(this IApplicationBuilder app)
    {
        RecurringJob.AddOrUpdate<IMediator>(a => a.Send(new AddVoteConfirmedBlock(), CancellationToken.None), "*/30 * * * * *");
    }
}


