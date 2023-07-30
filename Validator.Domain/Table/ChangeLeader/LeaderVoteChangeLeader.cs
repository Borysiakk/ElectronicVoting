using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Validator.Domain.Table.ChangeLeader.Base;

namespace Validator.Domain.Table.ChangeLeader;

public class LeaderVoteChangeLeader : VoteChangeLeader
{
    
}

public class LeaderVoteChangeLeaderConfiguration : IEntityTypeConfiguration<LeaderVoteChangeLeader>
{
    public void Configure(EntityTypeBuilder<LeaderVoteChangeLeader> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(a => a.Vote).IsRequired();
        builder.Property(a => a.ElectionChangeLeaderId).IsRequired();

        builder.HasIndex(a => a.ElectionChangeLeaderId);
    }
}
