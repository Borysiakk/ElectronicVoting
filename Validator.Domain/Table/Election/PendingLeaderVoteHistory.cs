using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Validator.Domain.Table.Election.Base;

namespace Validator.Domain.Table.Election;

public class PendingLeaderVoteHistory : PendingVoteHistory
{
    public PendingLeaderVoteHistory(string voteProcessId)
    {
        VoteProcessId = voteProcessId;
    }
}

public class PendingLeaderVoteHistoryConfiguration : IEntityTypeConfiguration<PendingLeaderVoteHistory>
{
    public void Configure(EntityTypeBuilder<PendingLeaderVoteHistory> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.VoteProcessId).IsRequired();
        builder.HasIndex(x => x.VoteProcessId);
    }
}