using ProtoBuf;
using System.Text.Json.Serialization;

namespace Validator.Domain.Table.Blockchain;

[ProtoContract]
public class Block
{
    [ProtoMember(1)]
    public Int64 BlockId { get; set; }
    [ProtoMember(2)]
    public byte[] Hash { get; set; }
    [ProtoMember(3)]
    public byte[]? PreviousHash { get; set; }
    [ProtoMember(4)]
    [JsonIgnore]
    public Int64 TransactionsId { get; set; }
    [ProtoIgnore]
    public ICollection<Transaction> Transactions { get; set; }
    public Block()
    {
        Transactions = new List<Transaction>();
    }

    public Block(byte[] previousHash)
    {
        PreviousHash = previousHash;
        Transactions = new List<Transaction>();
    }
}