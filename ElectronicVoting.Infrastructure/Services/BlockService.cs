using ElectronicVoting.Domain.Table.Blockchain;
using System.Security.Cryptography;
using ProtoBuf;
using ElectronicVoting.Persistence;

namespace ElectronicVoting.Infrastructure.Services
{

    public interface IBlockService
    {
        public Block Create();
        public Block AddTransaction(Block block, Transaction transaction);
    }

    public class BlockService : IBlockService
    {
        private readonly ApplicationDbContext _dbContext;

        public BlockService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Block AddTransaction(Block block, Transaction transaction)
        {
            block.Transactions.Add(transaction);

            return block;
        }

        public Block Create()
        {
            var block = new Block();
            block.Transactions = new List<Transaction>();

            var lastBlock = _dbContext.Blocks.OrderBy(a=>a.BlockId).LastOrDefault();
            if (lastBlock == null)
                block.PreviousHash = null;
            else
                block.PreviousHash = lastBlock.Hash;

            return block;
        }

    }
}