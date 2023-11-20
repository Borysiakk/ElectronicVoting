using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Validator.Domain.Table;

namespace Validator.Infrastructure.EntityFramework.Configuration;

public class SettingConfiguration : IEntityTypeConfiguration<Setting>
{
    public void Configure(EntityTypeBuilder<Setting> builder)
    {
        builder.HasKey(a => a.SettingId);

        builder.Property(a => a.Name).IsRequired();
        builder.Property(a => a.Value).IsRequired();
        builder.Property(a => a.ValueType).IsRequired();
        builder.Property(a => a.Category).IsRequired(false);

        builder.HasData(new Setting[]
        {
            new Setting()
            {
                SettingId = 1,
                Category = "Candidate",
                Name = "Lenght",
                Value = "2",
                ValueType = "Int64",
            },

            new Setting()
            {
                SettingId = 2,
                Category = "Voters",
                Name = "Lenght",
                Value = "255",
                ValueType = "Int64"
            },

            new Setting()
            {
                SettingId = 3,
                Category = "Approver",
                Name = "AcceptableValidatorsCount",
                Value = "3",
                ValueType = "Int64"
            }
        });

    }
}