namespace Validator.Domain.Table.ChangeLeader.Base;

public class PreVoteChangeLeader
{
    public Int64 Id { get; set; }
    public bool Decision { get; set; }
    public Int64 ApproverId { get; set; }
    public string PreElectionChangeLeaderId { get; set; }
}
