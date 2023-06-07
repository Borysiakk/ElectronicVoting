using ElectronicVoting.Domain.Table.Blockchain;
using ElectronicVoting.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicVoting.Infrastructure.Services
{
    public interface IBlochchainService
    {
        public Block CreateBlock();
        public Task SaveBlock(Block block, CancellationToken cancellationToken);
    }

    public class BlochchainService : IBlochchainService
    {

        private readonly BlockService _blockService;
        private readonly ApplicationDbContext _applicationDbContext;

        public BlochchainService(ApplicationDbContext applicationDbContext, BlockService blockService)
        {
            _blockService = blockService;
            _applicationDbContext = applicationDbContext;
        }

        public Block CreateBlock()
        {
            var block = new Block();

            var lastBlock = _applicationDbContext.Blocks.LastOrDefault();
            if (lastBlock == null)
                block.PreviousHash = null;
            else
                block.PreviousHash = lastBlock.Hash;

            return block;
            
        }

        public async Task SaveBlock(Block block, CancellationToken cancellationToken)
        {
            var data = _blockService.Serialize(block);
            block.Hash = _blockService.CalculateHash(data);

            await _applicationDbContext.Blocks.AddAsync(block, cancellationToken);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
