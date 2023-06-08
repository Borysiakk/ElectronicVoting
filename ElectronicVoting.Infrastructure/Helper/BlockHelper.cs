using ElectronicVoting.Domain.Table.Blockchain;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicVoting.Infrastructure.Helper
{
    public static class BlockHelper
    {
        public static byte[] CalculateHash(this Block block)
        {
            using (var sha512 = SHA512.Create())
            {
                using var stream = new MemoryStream();
                Serializer.Serialize(stream, block);
                return sha512.ComputeHash(stream.GetBuffer());
            }
        }
    }
}
