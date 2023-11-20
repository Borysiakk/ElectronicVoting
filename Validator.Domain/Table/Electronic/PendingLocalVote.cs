using Validator.Domain.Table.Electronic.Base;
namespace Validator.Domain.Table.Electronic;
public class PendingLocalVote : PendingVoteBase
{
    public long PendingLocalVoteId { get; set; }
}
