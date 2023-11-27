using Main.Domain.Table;
using Main.Infrastructure.EntityFramework.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Main.Infrastructure.EntityFramework;
public class MainDbContext : DbContext
{
    public DbSet<Candidate> Candidates { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CandidateConfiguration());
    }
}
