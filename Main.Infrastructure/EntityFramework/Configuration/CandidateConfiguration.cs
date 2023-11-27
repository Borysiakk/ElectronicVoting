using Main.Domain.Table;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Main.Infrastructure.EntityFramework.Configuration
{
    public class CandidateConfiguration : IEntityTypeConfiguration<Candidate>
    {
        public void Configure(EntityTypeBuilder<Candidate> builder)
        {
            builder.HasKey(a => a.CandidateId);
            builder.Property(a => a.Name).IsRequired();
            builder.Property(a => a.Value).IsRequired();

            //builder.HasData(new Approver[]
        }
    }
}
