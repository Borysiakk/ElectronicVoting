using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Validator.Domain.Table.Election;
public class VoteConfirmed
{
    public Int64 Id { get; set; }
    public byte[] Hash { get; set; }
    public bool IsInserted { get; set; }
    public string VoteProcessId { get; set; }
}


public class VoteConfirmedConfiguration : IEntityTypeConfiguration<VoteConfirmed>
{
    public void Configure(EntityTypeBuilder<VoteConfirmed> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Hash).IsRequired();
        builder.Property(x => x.VoteProcessId).IsRequired();
        builder.Property(x => x.IsInserted).HasDefaultValue(false);
        builder.HasIndex(x => x.VoteProcessId);
    }
}
