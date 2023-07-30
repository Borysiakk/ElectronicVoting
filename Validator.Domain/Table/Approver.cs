using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Validator.Domain.Table;

public class Approver
{
    public Int64 ApproverId { get; set; }
    public String Name { get; set; }
    public String NetworkAddress { get; set; }
}

public class ApproverConfiguration : IEntityTypeConfiguration<Approver>
{
    public void Configure(EntityTypeBuilder<Approver> builder)
    {
        builder.HasKey(a => a.ApproverId);
        builder.Property(a => a.Name).IsRequired();
        builder.Property(a => a.NetworkAddress).IsRequired();

        builder.HasData(new Approver[]
        {
            new Approver()
            {
                ApproverId = 1,
                Name = "ValidatorA",
                NetworkAddress = "http://validatorA:80",
            },
            new Approver()
            {
                ApproverId = 2,
                Name = "ValidatorB",
                NetworkAddress = "http://ValidatorB:80",
            },
            new Approver()
            {
                ApproverId = 3,
                Name = "ValidatorC",
                NetworkAddress = "http://ValidatorC:80",
            }
        });
    }
}