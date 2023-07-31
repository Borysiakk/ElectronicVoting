using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Validator.Domain.Table.Election;

public class PendingLeaderVote : PendingVote {}


public class PendingLeaderVoteConfiguration : IEntityTypeConfiguration<PendingLeaderVote>
{
    public void Configure(EntityTypeBuilder<PendingLeaderVote> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.VoteProcessId).IsRequired();

        builder.HasIndex(x => x.VoteProcessId);
    }
}
