namespace Validator.Domain.Table.ChangeLeader.Base;

public class VoteChangeLeader
{
    public Int64 Id { get; set; }
    public Int64 Vote { get; set; }
    public Int64 ApproverId { get; set; }
    public string ElectionChangeLeaderId { get; set; }
}
