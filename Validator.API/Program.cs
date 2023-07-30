using ElectronicVoting.Persistence;
using EntityFrameworkCore.Triggered;
using Hangfire;
using Hangfire.Redis.StackExchange;
using Microsoft.EntityFrameworkCore;
using ProtoBuf.Meta;
using System.Reflection;
using Validator.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var validatorName = Environment.GetEnvironmentVariable("CONTAINER_NAME");
string RedisConnectionStrings = Environment.GetEnvironmentVariable("REDIS_URL");
string DatebaseConnectionStrings = Environment.GetEnvironmentVariable("ConnectionStrings_Database");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructure();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddValidatorPersistence(option=> option.UseSqlServer(DatebaseConnectionStrings).UseTriggers(triggerOptions=> triggerOptions.AddAssemblyTriggers()));


builder.Services.AddHangfire(config =>
config.UseRedisStorage(RedisConnectionStrings, new RedisStorageOptions
{
    Prefix = "{hangfire}:"
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
app.UseHangfireDashboard();
app.MapControllers();

app.UseCors(builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
});

app.Run();
