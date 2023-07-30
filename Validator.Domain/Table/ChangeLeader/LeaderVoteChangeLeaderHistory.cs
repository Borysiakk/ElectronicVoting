using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Validator.Domain.Table.ChangeLeader.Base;

namespace Validator.Domain.Table.ChangeLeader;
public class LeaderVoteChangeLeaderHistory : VoteChangeLeaderHistory
{
}

public class LeaderVoteChangeLeaderHistoryConfiguration : IEntityTypeConfiguration<LeaderVoteChangeLeaderHistory>
{
    public void Configure(EntityTypeBuilder<LeaderVoteChangeLeaderHistory> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.ElectionChangeLeaderId).IsRequired();
    }
}