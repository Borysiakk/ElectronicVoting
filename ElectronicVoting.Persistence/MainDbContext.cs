using Main.Domain.Table;
using Microsoft.EntityFrameworkCore;
namespace ElectronicVoting.Persistence;

public class MainDbContext : DbContext
{
    public DbSet<Token> Tokens { get; set; }
    public MainDbContext(DbContextOptions<MainDbContext> options) : base(options){}
    
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        var connectionStringsVariable = Environment.GetEnvironmentVariable("ConnectionStrings_MainDb");
        builder.UseSqlServer(connectionStringsVariable);        
        
        base.OnConfiguring(builder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Token>(a =>
        {
            a.HasKey(b=>b.Id);
            a.Property(b => b.TokenValue).IsRequired();
            a.Property(b => b.Id).ValueGeneratedOnAdd();
        });

        base.OnModelCreating(builder);
    } 
}
