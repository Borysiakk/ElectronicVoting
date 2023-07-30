using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Validator.Domain.Table.ChangeLeader.Base;

namespace Validator.Domain.Table.ChangeLeader;
public class LocalVoteChangeLeaderHistory : VoteChangeLeaderHistory
{
    
}

public class LocalVoteChangeLeaderHistoryConfiguration : IEntityTypeConfiguration<LocalVoteChangeLeaderHistory>
{
    public void Configure(EntityTypeBuilder<LocalVoteChangeLeaderHistory> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.ElectionChangeLeaderId).IsRequired();
    }
}