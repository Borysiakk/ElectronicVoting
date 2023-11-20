using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Validator.Domain.Table.Electronic;

namespace Validator.Infrastructure.EntityFramework.Configuration
{
    public class PendingLeaderVoteConfiguration : IEntityTypeConfiguration<PendingLeaderVote>
    {
        public void Configure(EntityTypeBuilder<PendingLeaderVote> builder)
        {
            builder.HasKey(a => a.PendingLeaderVoteId);
        }
    }
}
