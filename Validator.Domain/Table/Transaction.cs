using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProtoBuf;

namespace Validator.Domain.Table;

[ProtoContract]
public class Transaction
{
    [ProtoMember(1)]
    public Int64 TransactionId { get; set; }
    [ProtoMember(2)]
    public Int64 Vote { get; set; }
    [ProtoMember(3)]
    public Int64 BlockId { get; set; }
    [ProtoIgnore]
    public Block Block { get; set; }

    public Transaction(Int64 vote)
    {
        Vote = vote;
    }
}

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
