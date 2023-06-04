using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicVoting.Domain
{
    public class Serialization
    {
        public byte[] Serialize()
        {
            using var stream = new MemoryStream();
            ProtoBuf.Serializer.Serialize(stream, this);
            return stream.GetBuffer();
        }
    }
}
