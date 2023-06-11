using ProtoBuf;

namespace ElectronicVoting.Validator.Domain.Table.BlockChain
{
    [ProtoContract]
    public class Block
    {
        [ProtoMember(1)]
        public Int64 BlockId { get; set; }
        [ProtoMember(2)]
        public byte [] Hash { get; set; }
        [ProtoMember(3)]
        public byte []? PreviousHash { get; set; }
        [ProtoMember(4)]
        public ICollection<Transaction> Transactions { get; set; }
    }
}
