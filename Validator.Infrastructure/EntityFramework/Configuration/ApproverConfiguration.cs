using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Validator.Domain.Table.Electronic;

namespace Validator.Infrastructure.EntityFramework.Configuration
{
    public class ApproverConfiguration : IEntityTypeConfiguration<Approver>
    {
        public void Configure(EntityTypeBuilder<Approver> builder)
        {
            builder.HasKey(a => a.ApproverId);
            builder.Property(a => a.Name).IsRequired();
            builder.Property(a => a.Address).IsRequired();

            builder.HasData(new Approver[]
            {
                new Approver()
                {
                    ApproverId = 1,
                    Name = "ValidatorA",
                    Address = "http://validatorA:80",
                },
                new Approver()
                {
                    ApproverId = 2,
                    Name = "ValidatorB",
                    Address = "http://ValidatorB:80",
                },
                new Approver()
                {
                    ApproverId = 3,
                    Name = "ValidatorC",
                    Address = "http://ValidatorC:80",
                }
            });
        }
    }
}
