using ElectronicVoting.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMainPersistence();
builder.Services.AddCommonPersistence();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


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
