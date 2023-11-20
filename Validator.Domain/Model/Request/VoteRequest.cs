using ProtoBuf;

namespace Validator.Domain.Model.Request;

[ProtoContract]
public class VoteRequest
{
    [ProtoMember(1)]
    public string Vote { get; set; }
}
