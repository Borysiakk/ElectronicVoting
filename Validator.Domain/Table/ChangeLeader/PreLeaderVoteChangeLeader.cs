

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Validator.Domain.Table.ChangeLeader.Base;

namespace Validator.Domain.Table.ChangeLeader;

public class PreLeaderVoteChangeLeader : PreVoteChangeLeader
{

}

public class PreLeaderVoteChangeLeaderConfiguration : IEntityTypeConfiguration<PreLeaderVoteChangeLeader>
{
    public void Configure(EntityTypeBuilder<PreLeaderVoteChangeLeader> builder)
    {
        builder.HasKey(a => a.Id);
        builder.HasIndex(a => a.PreElectionChangeLeaderId);
        builder.Property(a => a.ApproverId).IsRequired();
        builder.Property(a => a.PreElectionChangeLeaderId).IsRequired();
    }
}
