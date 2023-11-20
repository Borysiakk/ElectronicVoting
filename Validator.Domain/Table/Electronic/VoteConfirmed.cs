using System.Numerics;

namespace Validator.Domain.Table.Electronic;

public class VoteConfirmed
{
    public Int64 Id { get; set; }
    public string Vote { get; set; }
    public bool IsInserted { get; set; }
    public string SessionElectionId { get; set; }

    public VoteConfirmed() 
    {
        Vote = string.Empty;
        SessionElectionId = string.Empty;
    }

    public VoteConfirmed(string vote, string sessionElectionId)
    {
        Vote = vote;
        SessionElectionId = sessionElectionId;
    }
}