using ProtoBuf;
namespace ElectronicVoting.Validator.Domain.Contract.Request;

[ProtoContract]
public class ProofOfKnowledgeRequest : SerializationRequest
{
    [ProtoMember(1)]
    public long Voice { get; set; }
    [ProtoMember(2)]
    public string TransactionId { get; set; }
}
