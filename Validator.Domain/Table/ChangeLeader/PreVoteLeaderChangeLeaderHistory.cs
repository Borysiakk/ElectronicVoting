using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Validator.Domain.Table.ChangeLeader.Base;

namespace Validator.Domain.Table.ChangeLeader;

public class PreLeaderVoteChangeLeaderHistory : PreVoteChangeLeaderHistory
{

}

public class PreLeaderVoteChangeLeaderHistoryConfiguration : IEntityTypeConfiguration<PreLeaderVoteChangeLeaderHistory>
{
    public void Configure(EntityTypeBuilder<PreLeaderVoteChangeLeaderHistory> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.PreElectionChangeLeaderId).IsRequired();
    }
}