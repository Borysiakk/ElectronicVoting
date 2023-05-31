using ElectronicVoting.Domain.Table;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ElectronicVoting.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        private readonly MainDbContext _mainDbContext;
        public DbSet<TransactionPending> TransactionsPending { get; set; }
        public DbSet<TransactionRegister> TransactionRegisters { get; set; }
        public DbSet<PbftOperationConsensus> PbftOperationsConsensus { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options) { }
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, MainDbContext mainDbContext) :base(options)
        {
            _mainDbContext = mainDbContext;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<TransactionPending>(a =>
            {
                a.HasKey(b => b.Id);
                a.Property(b => b.Hash).IsRequired();
                a.Property(b => b.TransactionId).IsRequired();
                a.Property(b => b.Id).ValueGeneratedOnAdd();
            });

            builder.Entity<TransactionRegister>(a =>
            {
                a.HasKey(b => b.Id);
                a.Property(b => b.Id).ValueGeneratedOnAdd();
                a.Property(b => b.TransactionId).IsRequired();
            });

            builder.Entity<PbftOperationConsensus>(a =>
            {
                a.HasKey(b => b.Id);
                a.Property(b => b.Operations).IsRequired();
                a.Property(b => b.Status).IsRequired();
                a.Property(b => b.TransactionId).IsRequired(false);
                a.Property(b => b.Id).ValueGeneratedOnAdd();
            });

            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (_mainDbContext != null)
            {
                var name = Environment.GetEnvironmentVariable("CONTAINER_NAME");
                var connectionString = _mainDbContext.Validators.FirstOrDefault(a => a.Name == name)?.ConnectionString;
                builder.UseSqlServer(connectionString);
            }

            base.OnConfiguring(builder);
        }
        public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
        {
            public ApplicationDbContext CreateDbContext(string[] args)
            {
                var builder = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer(args.FirstOrDefault());
                return new ApplicationDbContext(builder.Options);
            }
        }
    }
}
