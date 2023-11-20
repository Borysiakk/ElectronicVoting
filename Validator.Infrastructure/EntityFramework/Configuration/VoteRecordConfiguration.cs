using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Validator.Domain.Table.Electronic;

namespace Validator.Infrastructure.EntityFramework.Configuration;

public class VoteRecordConfiguration : IEntityTypeConfiguration<VoteRecord>
{
    public void Configure(EntityTypeBuilder<VoteRecord> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Vote).IsRequired();
        builder.Property(a => a.SessionElectionId).IsRequired();
        builder.Property(a => a.IsInserted).HasDefaultValue(false);
        builder.Property(a => a.CreateDate).IsRequired();
    }
}
