using ElectronicVoting.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ElectronicVoting.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        
        public DbSet<VotingUser> VotingUsers { get;}
        public DbSet<ValidatorUser> ValidatorUsers { get;}
        public DbSet<ElectionCandidate> Candidates { get; }
        public DbSet<SessionValidator> SessionValidators { get;}
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
    
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer(DependencyInjection.DbConnection);
            return new ApplicationDbContext(builder.Options);
        }
    }
}