using ProtoBuf;
namespace Validator.Domain.Table.Blockchain;

[ProtoContract]
public class Transaction
{
    [ProtoMember(1)]
    public Int64 TransactionId { get; set; }
    [ProtoMember(2)]
    public Int64 Voice { get; set; }
    [ProtoMember(3)]
    public Int64 BlockId { get; set; }
    [ProtoMember(4)]
    public Block Block { get; set; }
}
