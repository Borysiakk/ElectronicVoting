
using ProtoBuf;
using System.Security.Cryptography;
using Validator.Domain.Table.Blockchain;

namespace ElectronicVoting.Validator.Infrastructure.Helper;
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
