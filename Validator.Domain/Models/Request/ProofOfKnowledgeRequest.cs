using ProtoBuf;

namespace Validator.Domain.Models.Request;

[ProtoContract]
public class ProofOfKnowledgeRequest
{
    [ProtoMember(1)]
    public long Vote { get; set; }
    [ProtoMember(2)]
    public string VoteProcessId { get; set; }
}
