using ElectronicVoting.Domain.Table.Blockchain;
using System.Security.Cryptography;
using ProtoBuf;
using ElectronicVoting.Persistence;

namespace ElectronicVoting.Infrastructure.Services
{

    public interface IBlockService
    {
        public Block Create();
    }

    public class BlockService : IBlockService
    {
        private readonly ApplicationDbContext _dbContext;

        public BlockService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Block Create()
        {
            var block = new Block();

            var lastBlock = _dbContext.Blocks.LastOrDefault();
            if (lastBlock == null)
                block.PreviousHash = null;
            else
                block.PreviousHash = lastBlock.Hash;

            return block;
        }

        private byte[] CalculateHash(byte[] data)
        {
            using(var sha512 = SHA512.Create())
            {
                return sha512.ComputeHash(data);
            }
        }

        private byte[] Serialize(Block block)
        {
            using(var stream = new MemoryStream())
            {
                Serializer.Serialize(stream, block);
                return stream.GetBuffer(); ;
            }
        }
    }
}