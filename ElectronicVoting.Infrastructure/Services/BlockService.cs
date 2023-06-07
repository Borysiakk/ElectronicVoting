using ElectronicVoting.Domain.Table.Blockchain;
using System.Security.Cryptography;
using ProtoBuf;
namespace ElectronicVoting.Infrastructure.Services
{

    public interface IBlockService
    {
        public byte[] Serialize(Block block);
        public byte[] CalculateHash(byte[] data);
    }

    public class BlockService : IBlockService
    {
        public byte[] CalculateHash(byte[] data)
        {
            using(var sha512 = SHA512.Create())
            {
                return sha512.ComputeHash(data);
            }
        }

        public byte[] Serialize(Block block)
        {
            using(var stream = new MemoryStream())
            {
                Serializer.Serialize(stream, block);
                return stream.GetBuffer(); ;
            }
        }
    }
}
