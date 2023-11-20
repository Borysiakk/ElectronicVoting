using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Validator.Domain.Table.Blockchain;
using Validator.Domain.Table.ChangeLeader;

namespace Validator.Infrastructure.EntityFramework.Configuration.ChangeLeader
{
    public class PreChangeLeaderVoteConfiguration : IEntityTypeConfiguration<PreChangeLeaderVote>
    {
        public void Configure(EntityTypeBuilder<PreChangeLeaderVote> builder)
        {
            builder.HasKey(e => e.PreChangeLeaderVoteId);
            builder.Property(a => a.SessionChangeLeaderVoteId).IsRequired();
            builder.Property(a => a.VotingApproverId).IsRequired();
            builder.Property(a => a.CurrentLeaderApproverId).IsRequired();
            builder.Property(a => a.Decision).IsRequired();
        }
    }
}
