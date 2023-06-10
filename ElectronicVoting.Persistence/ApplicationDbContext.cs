using ElectronicVoting.Domain.Table;
using ElectronicVoting.Domain.Table.Blockchain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ElectronicVoting.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Block> Blocks { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionPending> TransactionsPending { get; set; }
        public DbSet<TransactionRegister> TransactionRegisters { get; set; }
        public DbSet<TransactionConfirmed> TransactionsConfirmed { get; set; }
        public DbSet<PbftOperationConsensus> PbftOperationsConsensus { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options) { }
        

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<TransactionPending>(a =>
            {
                a.HasKey(b => b.Id);
                a.Property(b => b.Hash).IsRequired();
                a.Property(b => b.TransactionId).IsRequired();
                a.Property(b => b.Id).ValueGeneratedOnAdd();
            });

            builder.Entity<TransactionConfirmed>(a =>
            {
                a.HasKey(b => b.Id);
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
                a.HasIndex(b => b.Id);
                a.Property(b => b.Operations).IsRequired();
                a.Property(b => b.Status).IsRequired();
                a.Property(b => b.TransactionId).IsRequired(false);
                a.Property(b => b.Id).ValueGeneratedOnAdd();
            });

            builder.Entity<Block>(a =>
            {
                a.HasKey(b => b.BlockId);
                a.HasIndex(b => b.BlockId);
                a.Property(b => b.PreviousHash);
                a.HasMany<Transaction>(b => b.Transactions)
                 .WithOne(c => c.Block)
                 .HasForeignKey(c => c.BlockId)
                 .IsRequired();
            });

            builder.Entity<Transaction>(a =>
            {
                a.HasKey(b => b.TransactionId);
                a.Property(b => b.Voice).IsRequired();
                a.HasOne<Block>(b => b.Block)
                 .WithMany(c => c.Transactions)
                 .HasForeignKey(e => e.BlockId)
                 .IsRequired();
            });

            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            var connectionStringsVariable = Environment.GetEnvironmentVariable("ConnectionStrings");
            if (!String.IsNullOrEmpty(connectionStringsVariable))
            {
                builder.UseSqlServer(connectionStringsVariable);
            }
            else
            {
                var connectionStrings = GetConnectionString("appsettings.json", "DefaultConnection");
                builder.UseSqlServer(connectionStrings);
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

        private static string? GetConnectionString(string file, string name)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile(file, false).Build();

            return configuration.GetConnectionString(name);
        }
    }
}
