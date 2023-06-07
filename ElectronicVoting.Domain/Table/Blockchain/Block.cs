using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicVoting.Domain.Table.Blockchain
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
        public Int64 TransactionsId { get; set; }
        [ProtoMember(5)]
        public ICollection<Transaction> Transactions { get; set; }
    }
}
