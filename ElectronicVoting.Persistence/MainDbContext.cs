using Main.Domain.Table;
using Microsoft.EntityFrameworkCore;

namespace ElectronicVoting.Persistence;

public class MainDbContext : DbContext
{
    public DbSet<CurrentLeader> CurrentLeaders { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CurrentLeaderConfiguration());
    }
}
