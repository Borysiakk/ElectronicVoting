using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Validator.Domain.Table.Blockchain;

namespace Validator.Infrastructure.EntityFramework.Configuration;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.HasKey(b => b.TransactionId);
        builder.Property(b => b.Vote).IsRequired();
        builder.HasOne<Block>(b => b.Block)
               .WithMany(c => c.Transactions)
               .HasForeignKey(e => e.BlockId)
               .IsRequired();
    }
}