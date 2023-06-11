using Microsoft.EntityFrameworkCore;
namespace ElectronicVoting.Persistence;

public class MainDbContext : DbContext
{

    public MainDbContext(DbContextOptions<MainDbContext> options) : base(options){}
    
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        var connectionStringsVariable = Environment.GetEnvironmentVariable("ConnectionStrings_MainDb");
        builder.UseSqlServer(connectionStringsVariable);        
        
        base.OnConfiguring(builder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    } 
}
