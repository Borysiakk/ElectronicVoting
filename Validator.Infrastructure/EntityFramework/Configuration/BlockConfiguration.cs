using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Validator.Domain.Table.Blockchain;

namespace Validator.Infrastructure.EntityFramework.Configuration;

public class BlockConfiguration : IEntityTypeConfiguration<Block>
{
    public void Configure(EntityTypeBuilder<Block> builder)
    {
        builder.HasKey(b => b.BlockId);

        builder.Property(b => b.PreviousHash);
        builder.HasMany<Transaction>(b => b.Transactions)
               .WithOne(c => c.Block)
               .HasForeignKey(c => c.BlockId)
               .IsRequired();
    }
}