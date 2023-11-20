
using ProtoBuf;
using System.Text.Json.Serialization;

namespace Validator.Domain.Table.Blockchain;

[ProtoContract]
public class Transaction
{
    [ProtoMember(1)]
    public Int64 TransactionId { get; set; }
    [ProtoMember(2)]
    public string Vote { get; set; }
    [ProtoMember(3)]
    public Int64 BlockId { get; set; }
    [JsonIgnore]
    [ProtoIgnore]
    public Block Block { get; set; }
}