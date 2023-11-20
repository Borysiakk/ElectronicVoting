using ProtoBuf;

namespace Validator.Domain.Model.Request;

[ProtoContract]
public class ProofOfKnowledgeRequest
{
    [ProtoMember(1)]
    public VoteRequest Vote { get; set; }
    [ProtoMember(2)]
    public string SessionElectionId { get; set; }

    public ProofOfKnowledgeRequest(VoteRequest vote, string sessionElectionId)
    {
        Vote = vote;
        SessionElectionId = sessionElectionId;
    }
}
