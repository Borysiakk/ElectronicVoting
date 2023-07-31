namespace Validator.Domain.Table.Election;

public class PendingVote
{
    public Int64 Id { get; set; }
    public byte[] Hash { get; set; }
    public string VoteProcessId { get; set; }
}
