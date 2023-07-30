using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Validator.Domain.Table.ChangeLeader.Base;

namespace Validator.Domain.Table.ChangeLeader;

public class PreLocalVoteChangeLeaderHistory : PreVoteChangeLeaderHistory
{

}

public class PreLocalVoteChangeLeaderHistoryConfiguration : IEntityTypeConfiguration<PreLocalVoteChangeLeaderHistory>
{
    public void Configure(EntityTypeBuilder<PreLocalVoteChangeLeaderHistory> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.PreElectionChangeLeaderId).IsRequired();
    }
}