using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Main.Domain.Table;

public record Validator(Int64 ApproverId, string Name,string Address);

public class ValidatorConfiguration : IEntityTypeConfiguration<Validator>
{
    public void Configure(EntityTypeBuilder<Validator> builder)
    {
        builder.HasKey(a => a.ApproverId);
        builder.Property(a => a.Name).IsRequired();
        builder.Property(a => a.Address).IsRequired();

        builder.HasData(new Validator[]
        {
            new Validator(1, "ValidatorA", "http://validatorA:80"),
            new Validator(2, "ValidatorB", "http://validatorB:80"),
            new Validator(3, "ValidatorC", "http://validatorC:80"),
        });
    }
}
