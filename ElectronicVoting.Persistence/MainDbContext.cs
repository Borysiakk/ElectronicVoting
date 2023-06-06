using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElectronicVoting.Domain.Table;
using ElectronicVoting.Domain.Table.Main;

namespace ElectronicVoting.Persistence
{
    public class MainDbContext : DbContext
    {
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Validator> Validators { get; set; }
        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
        {

        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            var connectionStrings = GetConnectionString("appsettings.json", "DefaultConnection");
            builder.UseSqlServer(connectionStrings);
            
            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Validator>(a =>
            {
                a.HasKey(b => b.Id);
                a.Property(b => b.Name).IsRequired();
                a.Property(b => b.Address).IsRequired();
                a.Property(b => b.ConnectionString).IsRequired();
                a.Property(b => b.ConnectionStringToBuild).IsRequired();
                a.Property(b => b.Id).ValueGeneratedOnAdd();

                a.HasData(new Validator[]
                {
                    new Validator()
                    {
                        Id = 1,
                        Name = "ValidatorA",
                        Address = "http://electronicvoting.api:80",
                        ConnectionString = "Server=DatabaseA;User Id=sa;Password=LitwoOjczyznoMoja1234@;TrustServerCertificate=true",
                        ConnectionStringToBuild = "Server=localhost,8091;User Id=sa;Password=LitwoOjczyznoMoja1234@;TrustServerCertificate=true"
                    },
                    new Validator()
                    {
                        Id = 2,
                        Name = "ValidatorB",
                        Address = "http://ValidatorB:80",
                        ConnectionString = "Server=DatabaseB;User Id=sa;Password=LitwoOjczyznoMoja1234@;TrustServerCertificate=true",
                        ConnectionStringToBuild = "Server=localhost,8092;User Id=sa;Password=LitwoOjczyznoMoja1234@;TrustServerCertificate=true"
                    }
                });
            });

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
        }
        
        public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<MainDbContext>
        {
            public MainDbContext CreateDbContext(string[] args)
            {

                var connectionStrings = GetConnectionString("appsettings.json", "DefaultConnection");

                var builder = new DbContextOptionsBuilder<MainDbContext>().UseSqlServer(connectionStrings);
                return new MainDbContext(builder.Options);
            }
        }

        private static string ? GetConnectionString(string file,string name)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile(file, false).Build();

            return configuration.GetConnectionString(name);
        }
        
    }
}
