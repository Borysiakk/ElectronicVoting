using ProtoBuf;

namespace ElectronicVoting.Validator.Domain.Contract.Result;

[ProtoContract]
public class ProofOfKnowledgeResult :SerializationResult
{
    [ProtoMember(1)]
    public string TransactionId { get; set; }
    [ProtoMember(2)]
    public byte[] Hash { get; set; }
}
