using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Validator.Domain.Table.Election;

public class VoteRecord
{
    public Int64 Id { get; set; }
    public bool IsInserted {get;set;}
    public string VoteProcessId { get; set; }
    public DateTime CreateDate { get; set; }

    public VoteRecord()
    {
        CreateDate = DateTime.Now;
    }
}


public class VoteRecordConfiguration : IEntityTypeConfiguration<VoteRecord>
{
    public void Configure(EntityTypeBuilder<VoteRecord> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.VoteProcessId).IsRequired();
        builder.Property(a => a.IsInserted).HasDefaultValue(false);
        builder.Property(a => a.CreateDate).IsRequired();
    }
}