using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Validator.Domain.Table.Electronic;

namespace Validator.Infrastructure.EntityFramework.Configuration
{
    public class PendingLocalVoteConfiguration : IEntityTypeConfiguration<PendingLocalVote>
    {
        public void Configure(EntityTypeBuilder<PendingLocalVote> builder)
        {
            builder.HasKey(a => a.PendingLocalVoteId);
        }
    }
}
