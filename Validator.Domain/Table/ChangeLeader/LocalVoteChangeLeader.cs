using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Validator.Domain.Table.ChangeLeader.Base;

namespace Validator.Domain.Table.ChangeLeader;

public class LocalVoteChangeLeader : VoteChangeLeader
{

}

public class LocalVoteChangeLeaderConfiguration : IEntityTypeConfiguration<LocalVoteChangeLeader>
{
    public void Configure(EntityTypeBuilder<LocalVoteChangeLeader> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(a => a.Vote).IsRequired();
        builder.Property(a => a.ElectionChangeLeaderId).IsRequired();
        builder.Property(a => a.ApproverId).IsRequired();

        builder.HasIndex(a => a.ElectionChangeLeaderId);
    }
}
