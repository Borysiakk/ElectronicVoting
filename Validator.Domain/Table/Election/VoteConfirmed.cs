using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Validator.Domain.Table.Election;
public class VoteConfirmed
{
    public Int64 Id { get; set; }
    public Int64 Vote { get; set; }
    public byte[] Hash { get; set; }
    public bool IsInserted { get; set; }
    public string VoteProcessId { get; set; }

    public VoteConfirmed(Int64 vote, byte[] hash, string voteProcessId)
    {
        Vote = vote;
        Hash = hash;
        VoteProcessId = voteProcessId;
    }
}

public class VoteConfirmedConfiguration : IEntityTypeConfiguration<VoteConfirmed>
{
    public void Configure(EntityTypeBuilder<VoteConfirmed> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Hash).IsRequired(false);
        builder.Property(x => x.VoteProcessId).IsRequired();
        builder.Property(x => x.IsInserted).HasDefaultValue(false);
        builder.HasIndex(x => x.VoteProcessId);
    }
}
