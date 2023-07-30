

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Validator.Domain.Table.ChangeLeader;

public class Leader
{
    public Int64 LeaderId { get; set; }
    public Int64 ChangeAproverId { get; set; }
    public Int64 LeaderApproverId { get; set; }

    public string PreElectionId { get;set; }    
}


public class LeaderConfiguration : IEntityTypeConfiguration<Leader>
{
    public void Configure(EntityTypeBuilder<Leader> builder)
    {
        builder.HasKey(e => e.LeaderId);
        builder.Property(a => a.ChangeAproverId).IsRequired();
        builder.Property(a => a.LeaderApproverId).IsRequired();

        builder.Property(a => a.PreElectionId).IsRequired();

        builder.HasData(new Leader[]
        {
            new Leader()
            {
                LeaderId = 1,
                LeaderApproverId = 1,
                ChangeAproverId = 1,
                PreElectionId = "1234-12345",
            }
        });
    }
}