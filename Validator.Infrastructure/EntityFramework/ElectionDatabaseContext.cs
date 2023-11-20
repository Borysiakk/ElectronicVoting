using Microsoft.EntityFrameworkCore;
using Validator.Domain.Table;
using Validator.Domain.Table.Blockchain;
using Validator.Domain.Table.ChangeLeader;
using Validator.Domain.Table.Electronic;
using Validator.Infrastructure.EntityFramework.Configuration;
using Validator.Infrastructure.EntityFramework.Configuration.ChangeLeader;
namespace Validator.Infrastructure.EntityFramework;

public class ElectionDatabaseContext : DbContext
{
    public ElectionDatabaseContext(DbContextOptions options) : base(options) {}

    public DbSet<Setting> Settings { get; set; }
    //Blockchain
    public DbSet<Block> Blocks { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    //Change leader
    public DbSet<ChangeLeaderVote> ChangeLeaderVotes { get; set; }
    public DbSet<PreChangeLeaderVote> PreChangeLeaderVotes { get; set; }

    //Election
    public DbSet<Leader> Leaders { get; set; }
    public DbSet<Approver> Approvers { get; set; }
    public DbSet<VoteRecord> VoteRecords { get; set; }
    public DbSet<VoteConfirmed> VoteConfirmeds { get; set; }
    public DbSet<PendingLocalVote> PendingLocalVotes { get; set; }
    public DbSet<PendingLeaderVote> PendingLeaderVotes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {


        modelBuilder.ApplyConfiguration(new SettingConfiguration());
        //Blockchain
        modelBuilder.ApplyConfiguration(new BlockConfiguration());
        modelBuilder.ApplyConfiguration(new TransactionConfiguration());
        //Election
        modelBuilder.ApplyConfiguration(new LeaderConfiguration());
        modelBuilder.ApplyConfiguration(new ApproverConfiguration());
        modelBuilder.ApplyConfiguration(new VoteRecordConfiguration());
        modelBuilder.ApplyConfiguration(new VoteConfirmedConfiguration());
        //Change Leader
        modelBuilder.ApplyConfiguration(new ChangeLeaderVoteConfiguration());
        modelBuilder.ApplyConfiguration(new PreChangeLeaderVoteConfiguration());

        modelBuilder.ApplyConfiguration(new PendingLocalVoteConfiguration());
        modelBuilder.ApplyConfiguration(new PendingLeaderVoteConfiguration());

        modelBuilder.ApplyConfiguration(new VoteConfirmedConfiguration());
    }
}
