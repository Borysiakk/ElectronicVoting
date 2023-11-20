namespace Validator.Domain.Table.ChangeLeader;

public class PreChangeLeaderVote
{
    public string PreChangeLeaderVoteId { get; set; }
    public string SessionChangeLeaderVoteId { get; set; }
    public bool Decision { get; set; }
    public long VotingApproverId { get; set; }
    public long CurrentLeaderApproverId { get; set; }

}
