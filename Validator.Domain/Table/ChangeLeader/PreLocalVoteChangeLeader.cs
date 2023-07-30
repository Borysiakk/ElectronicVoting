using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Validator.Domain.Table.ChangeLeader.Base;

namespace Validator.Domain.Table.ChangeLeader;

public class PreLocalVoteChangeLeader : PreVoteChangeLeader
{

}

public class PreLocalVoteChangeLeaderConfiguration : IEntityTypeConfiguration<PreLocalVoteChangeLeader>
{
    public void Configure(EntityTypeBuilder<PreLocalVoteChangeLeader> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Decision).IsRequired();
        builder.Property(a => a.ApproverId).IsRequired();
        builder.Property(a => a.PreElectionChangeLeaderId).IsRequired();
        builder.HasIndex(a => a.PreElectionChangeLeaderId);
    }
}