using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicVoting.Domain.Contract.Result
{
    public class ProofOfKnowledgeResult :SerializationResult
    {
        [ProtoMember(1)]
        public string TransactionId { get; set; }
        [ProtoMember(2)]
        public byte[] Hash { get; set; }
    }
}
