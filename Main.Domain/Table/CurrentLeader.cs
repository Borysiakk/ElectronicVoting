using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Main.Domain.Table;

public record CurrentLeader(string Name, string Address)
{
    public Int64 Id { get; init; }

    public CurrentLeader(Int64 id, string name, string address): this(name,address)
    {
        Id = id;
    }
}

public class CurrentLeaderConfiguration : IEntityTypeConfiguration<CurrentLeader>
{
    public void Configure(EntityTypeBuilder<CurrentLeader> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.Address).IsRequired();
    }
}