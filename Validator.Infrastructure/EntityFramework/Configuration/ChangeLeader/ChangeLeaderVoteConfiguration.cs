using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Validator.Domain.Table.ChangeLeader;

namespace Validator.Infrastructure.EntityFramework.Configuration.ChangeLeader;

public class ChangeLeaderVoteConfiguration : IEntityTypeConfiguration<ChangeLeaderVote>
{
    public void Configure(EntityTypeBuilder<ChangeLeaderVote> builder)
    {
        builder.HasKey(e => e.ChangeLeaderVoteId);
        builder.Property(a => a.SessionChangeLeaderVoteId).IsRequired();
        builder.Property(a => a.CandidateLeaderApproverId).IsRequired();
        builder.Property(a => a.CurrentLeaderApproverId).IsRequired();
        builder.Property(a => a.VotingApproverId).IsRequired();
    }
}
