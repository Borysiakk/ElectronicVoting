using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Validator.Domain.Table.Election;

public class PendingLocalVote : PendingVote {}

public class PendingLocalVoteConfiguration : IEntityTypeConfiguration<PendingLocalVote>
{
    public void Configure(EntityTypeBuilder<PendingLocalVote> builder)
    {   
        builder.HasKey(x => x.Id);
        builder.Property(x => x.VoteProcessId).IsRequired();
        builder.HasIndex(x => x.VoteProcessId);
    }
}
