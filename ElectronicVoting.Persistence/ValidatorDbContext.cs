using Microsoft.EntityFrameworkCore;
using Validator.Domain.Table;
using Validator.Domain.Table.ChangeLeader;
using Validator.Domain.Table.ChangeLeader.Base;
using Validator.Domain.Table.Election;

namespace ElectronicVoting.Persistence;

public class ValidatorDbContext : DbContext
{
    public DbSet<Setting> Settings { get; set; }
    public DbSet<Approver> Approvers { get; set; }

    //Election
    public DbSet<VoteRecord> VoteRecords { get; set; }

    //ChangeLeader
    public DbSet<Leader> Leaders { get; set; }
    public DbSet<LocalVoteChangeLeader> LocalVoteChangeLeaders { get; set; }
    public DbSet<LeaderVoteChangeLeader> LeaderVoteChangeLeaders { get; set; }
    public DbSet<PreLocalVoteChangeLeader> PreLocalVoteChangeLeaders { get; set; }
    public DbSet<PreLeaderVoteChangeLeader> PreLeaderVoteChangeLeaders { get; set; }
    public DbSet<LocalVoteChangeLeaderHistory> LocalVoteChangeLeaderHistories { get; set; }
    public DbSet<LeaderVoteChangeLeaderHistory> LeaderVoteChangeLeaderHistories { get; set; }
    public DbSet<PreLocalVoteChangeLeaderHistory> PreLocalVoteChangeLeaderHistories { get; set; }
    public DbSet<PreLeaderVoteChangeLeaderHistory> PreLeaderVoteChangeLeaderHistories { get; set; }

    public ValidatorDbContext(DbContextOptions<ValidatorDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new SettingConfiguration());
        modelBuilder.ApplyConfiguration(new ApproverConfiguration());

        //ChangeLeader
        modelBuilder.ApplyConfiguration(new LeaderConfiguration());

        modelBuilder.ApplyConfiguration(new PreLocalVoteChangeLeaderConfiguration());
        modelBuilder.ApplyConfiguration(new PreLeaderVoteChangeLeaderConfiguration());
        modelBuilder.ApplyConfiguration(new PreLocalVoteChangeLeaderHistoryConfiguration());
        modelBuilder.ApplyConfiguration(new PreLeaderVoteChangeLeaderHistoryConfiguration());

        modelBuilder.ApplyConfiguration(new LocalVoteChangeLeaderConfiguration());
        modelBuilder.ApplyConfiguration(new LeaderVoteChangeLeaderConfiguration());
        modelBuilder.ApplyConfiguration(new LocalVoteChangeLeaderConfiguration());
        modelBuilder.ApplyConfiguration(new LocalVoteChangeLeaderHistoryConfiguration());
        modelBuilder.ApplyConfiguration(new LeaderVoteChangeLeaderHistoryConfiguration());

        //Election
        modelBuilder.ApplyConfiguration(new VoteRecordConfiguration());

    }

}
