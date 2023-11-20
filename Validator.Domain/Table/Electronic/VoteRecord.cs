using System.Numerics;

namespace Validator.Domain.Table.Electronic;

public class VoteRecord
{
    public long Id { get; set; }
    public string Vote { get; set; }
    public bool IsInserted { get; set; }
    public DateTime CreateDate { get; set; }
    public string SessionElectionId { get; set; }

    public VoteRecord()
    {
        Vote = string.Empty;
        SessionElectionId = string.Empty;
    }

    public VoteRecord(string vote, string sessionElectionId)
    {
        Vote = vote;
        CreateDate = DateTime.Now;
        SessionElectionId = sessionElectionId;
    }
}
