using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Validator.Domain.Table.Electronic;

namespace Validator.Infrastructure.EntityFramework.Configuration;

public class VoteConfirmedConfiguration : IEntityTypeConfiguration<VoteConfirmed>
{
    public void Configure(EntityTypeBuilder<VoteConfirmed> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.SessionElectionId).IsRequired();
        builder.Property(x => x.IsInserted).HasDefaultValue(false);
        builder.HasIndex(x => x.SessionElectionId);
    }
}
