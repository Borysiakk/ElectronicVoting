using ElectronicVoting.Persistence;
using Validator.Domain.Table.Blockchain;

namespace ElectronicVoting.Infrastructure.Services
{

    public interface IBlockService
    {
        public Block Create();
        public Block AddTransaction(Block block, Transaction transaction);
    }

    public class BlockService : IBlockService
    {
        private readonly ValidatorDbContext _dbContext;

        public BlockService(ValidatorDbContext dbContext)
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