using Microsoft.EntityFrameworkCore;
using ElectronicVoting.Common.Domain.Table;

namespace ElectronicVoting.Persistence;

public class CommonDbContext : DbContext
{
    public DbSet<Setting> Settings { get; set; }

    public CommonDbContext(DbContextOptions<CommonDbContext> options) : base(options) { }


    protected override void OnModelCreating(ModelBuilder builder)
    {

        builder.Entity<Setting>(a =>
        {
            a.HasKey(b => b.Id);
            a.Property(b => b.Name).IsRequired();
            a.Property(b => b.Value).IsRequired();
            a.Property(b => b.Id).ValueGeneratedOnAdd();
            a.HasData(new Setting[]
            {
                    new Setting()
                    {
                        Id = 1,
                        Name = "Candidate",
                        SubName = "Count",
                        Value = "0",
                    },
                    new Setting()
                    {
                        Id = 2,
                        Name = "Voters",
                        SubName = "Count",
                        Value = "255",
                    },
                    new Setting()
                    {
                        Id = 3,
                        Name = "Validator",
                        SubName = "AcceptableValidatorsCount",
                        Value = "2"
                    },
            });
        });

        base.OnModelCreating(builder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        var connectionStringsVariable = Environment.GetEnvironmentVariable("ConnectionStrings_CommonDb");
        builder.UseSqlServer(connectionStringsVariable);

        base.OnConfiguring(builder);
    }
}
