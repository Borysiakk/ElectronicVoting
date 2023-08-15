using ElectronicVoting.Persistence;
using Hangfire;
using Hangfire.Redis.StackExchange;
using Microsoft.EntityFrameworkCore;
using Validator.Infrastructure;
using Validator.Infrastructure.Hangfire;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var hangfireJobsInitializer = new HangfireJobsInitializer();
var validatorName = Environment.GetEnvironmentVariable("CONTAINER_NAME");
string RedisConnectionStrings = Environment.GetEnvironmentVariable("REDIS_URL");
string DatebaseConnectionStrings = Environment.GetEnvironmentVariable("ConnectionStrings_Database");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructure();
builder.Services.AddValidatorPersistence(option=> option.UseSqlServer(DatebaseConnectionStrings).UseTriggers(triggerOptions=> triggerOptions.AddAssemblyTriggers()));


builder.Services.AddHangfire(config =>
config.UseRedisStorage(RedisConnectionStrings, new RedisStorageOptions
{
}));

builder.Services.AddHangfireServer();
var app = builder.Build();



using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ValidatorDbContext>();
    dbContext.Database.EnsureCreated();
    //dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.UseHangfireDashboard("/hangfire", new DashboardOptions
{
    Authorization = new[] { new HangfireOpenAuthorizationFilter() }
});
app.MapControllers();

app.UseCors(builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
});

hangfireJobsInitializer.InitializeJobs();
app.Run();