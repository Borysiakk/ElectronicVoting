using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Validator.Domain.Table.Electronic;

namespace Validator.Infrastructure.EntityFramework.Configuration
{
    public sealed class LeaderConfiguration : IEntityTypeConfiguration<Leader>
    {
        public void Configure(EntityTypeBuilder<Leader> builder)
        {
            builder.HasKey(e => e.LeaderId);
            builder.Property(a => a.ApproverId).IsRequired();
            builder.Property(a => a.SessionId).IsRequired();

            builder.HasData(new Leader[]
            {
                new Leader()
                {
                    LeaderId = 1,
                    ApproverId = 1,
                    SessionId = "",
                }
            });
        }
    }
}
