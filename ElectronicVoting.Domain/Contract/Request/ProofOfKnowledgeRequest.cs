using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicVoting.Domain.Contract.Request
{
    [ProtoContract]
    public class ProofOfKnowledgeRequest : SerializationRequest
    {
        [ProtoMember(1)]
        public long Voice { get; set; }
        [ProtoMember(2)]
        public string TransactionId { get; set; }
    }
}
