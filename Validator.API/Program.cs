 using Hangfire;
using System.Reflection;
using Validator.Application.Handler;
using Validator.Application.Handler.Query.Blockchain;
using Validator.Application.Service;
using Validator.Infrastructure.Cache;
using Validator.Infrastructure.EntityFramework;
using Validator.Infrastructure.Hangfire;
using Validator.Infrastructure.Hangfire.Handler;
using Validator.Infrastructure.Repository;
using Validator.Infrastructure.RestSharp;
using Validator.Infrastructure.Serilog;

namespace Validator.API;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.WebHost.UseLogging();

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddCache();
        builder.Services.AddServices();
        builder.Services.AddRepositories();
        builder.Services.AddEntityFramework();
        builder.Services.AddRestClient();
        builder.Services.AddMediatR();
        builder.Services.AddHangfire();
        builder.Services.AddHttpClient();

        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
        Assembly.GetExecutingAssembly(),
        typeof(AddVoteConfirmedBlock).Assembly,
        typeof(GetBlocks).Assembly,
        typeof(GetTransactions).Assembly));

        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<ElectionDatabaseContext>();
            dbContext.Database.EnsureCreated();
            //dbContext.Database.Migrate();
        }

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHangfireDashboard("/hangfire", new DashboardOptions
        {
            Authorization = new[] { new HangfireOpenAuthorizationFilter() }
        });

        //app.UseHttpsRedirection();

        //app.UseAuthorization();

        app.ConfigureHangFire();

        app.MapControllers();

        app.Run();
    }
}