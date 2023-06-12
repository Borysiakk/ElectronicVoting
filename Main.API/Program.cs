using ElectronicVoting.Persistence;
using Main.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMainPersistence();
builder.Services.AddCommonPersistence();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructure();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var mainDbContext = scope.ServiceProvider.GetRequiredService<MainDbContext>();
    var commonDbContext = scope.ServiceProvider.GetRequiredService<CommonDbContext>();

    mainDbContext.Database.EnsureCreated();
    commonDbContext.Database.EnsureCreated();
}



    // Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
