

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Validator.Domain.Table.Election.Base;

namespace Validator.Domain.Table.Election;
public class PendingLocalVoteHistory : PendingVoteHistory
{
    public PendingLocalVoteHistory()
    {

    }
}

public class PendingLocalVoteHistoryConfiguration : IEntityTypeConfiguration<PendingLocalVoteHistory>
{
    public void Configure(EntityTypeBuilder<PendingLocalVoteHistory> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.VoteProcessId).IsRequired();
        builder.HasIndex(x => x.VoteProcessId);
    }
}