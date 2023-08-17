using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProtoBuf;

namespace Validator.Domain.Table;

// When we add a new block its hash is calculated in the BeforeCreateBlockTrigger.cs trigger

[ProtoContract]
public class Block
{
    [ProtoMember(1)]
    public Int64 BlockId { get; set; }
    [ProtoMember(2)]
    public byte[] Hash { get; set; }
    [ProtoMember(3)]
    public byte[]? PreviousHash { get; set; }
    [ProtoIgnore]
    public ICollection<Transaction> Transactions { get; set; }
    public Block()
    {
        Transactions = new List<Transaction>();
    }

    public Block(byte[] previousHash)
    {
        PreviousHash = previousHash;
        Transactions = new List<Transaction>();
    }
}

public class BlockConfiguration : IEntityTypeConfiguration<Block>
{
    public void Configure(EntityTypeBuilder<Block> builder)
    {
        builder.HasKey(b => b.BlockId);
        builder.Property(b => b.PreviousHash);
        builder.HasMany<Transaction>(b => b.Transactions)
               .WithOne(c => c.Block)
               .IsRequired();
    }
}
