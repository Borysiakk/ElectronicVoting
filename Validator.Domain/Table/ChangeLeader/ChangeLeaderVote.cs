namespace Validator.Domain.Table.ChangeLeader;
public class ChangeLeaderVote
{
    public string ChangeLeaderVoteId { get; set; }
    public string SessionChangeLeaderVoteId { get; set; }
    public long VotingApproverId { get; set; }
    public long CandidateLeaderApproverId { get; set; }
    public long CurrentLeaderApproverId { get; set; }
}
