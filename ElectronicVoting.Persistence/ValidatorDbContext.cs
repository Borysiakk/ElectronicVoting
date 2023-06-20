
using Microsoft.EntityFrameworkCore;
using Validator.Domain.Table;
using Validator.Domain.Table.Blockchain;
using Validator.Domain.Table.ChangeView;

namespace ElectronicVoting.Persistence;

public class ValidatorDbContext : DbContext
{
    public DbSet<Block> Blocks { get; set; }
    public DbSet<Approver> Approvers { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<TransactionPending> TransactionsPending { get; set; }
    public DbSet<TransactionRegister> TransactionRegisters { get; set; }
    public DbSet<TransactionConfirmed> TransactionsConfirmed { get; set; }
    public DbSet<PbftOperationConsensus> PbftOperationsConsensus { get; set; }

    public DbSet<ChangeViewTransaction> ChangeViewTransactions { get; set; }
    public DbSet<InitializationChangeViewTransaction> InitializationChangeViewTransactions { get; set; }

    public ValidatorDbContext(DbContextOptions<ValidatorDbContext> options) :base(options) { }
    

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

        builder.Entity<Approver>(a =>
        {
            a.HasKey(b => b.Id);
            a.HasMany(e => e.InitializationChangeViewTransaction)
             .WithOne(e => e.Approver)
             .HasForeignKey(e => e.ApproverId);

            a.HasMany(e => e.ChangeViewTransaction)
             .WithOne(e => e.Approver)
             .HasForeignKey(e => e.ApproverId);

            a.HasMany(e => e.SelectedChangeViewTransaction)
             .WithOne(e => e.SelectedApprover)
             .HasForeignKey(e => e.SelectedApproverId)
             .OnDelete(DeleteBehavior.Restrict);

            a.Property(b => b.Name).IsRequired();
            a.Property(b => b.Address).IsRequired();
            a.Property(b => b.Id).ValueGeneratedOnAdd();

            a.HasData(new Approver[]
            {
                new Approver()
                {
                    Id = 1,
                    Name = "ValidatorA",
                    Address = "http://validatorA:80",
                },
                new Approver()
                {
                    Id = 2,
                    Name = "ValidatorB",
                    Address = "http://ValidatorB:80",
                }
            });
        });

        builder.Entity<InitializationChangeViewTransaction>(a =>
        {
            a.HasKey(b => b.Id);
            a.HasOne(e => e.Approver)
             .WithMany(e => e.InitializationChangeViewTransaction)
             .HasForeignKey(e => e.ApproverId)
             .IsRequired(false);

        });

        builder.Entity<ChangeViewTransaction>(a =>
        {
            a.HasKey(b => b.Id);
            a.HasOne(e => e.Approver)
             .WithMany(e => e.ChangeViewTransaction)
             .HasForeignKey(e => e.ApproverId)
             .IsRequired(false)
             .OnDelete(DeleteBehavior.Restrict);

            a.HasOne(e => e.SelectedApprover)
             .WithMany(e => e.SelectedChangeViewTransaction)
             .HasForeignKey(e => e.SelectedApproverId)
             .IsRequired(false)
             .OnDelete(DeleteBehavior.Restrict);
        });

        base.OnModelCreating(builder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        var connectionStringsVariable = Environment.GetEnvironmentVariable("ConnectionStrings_Database");
        builder.UseSqlServer(connectionStringsVariable);

        base.OnConfiguring(builder);
    }
}
