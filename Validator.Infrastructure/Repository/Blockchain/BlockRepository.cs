using ElectronicVoting.Persistence;
using Microsoft.EntityFrameworkCore;
using Validator.Domain.Table;

namespace Validator.Infrastructure.Repository.Blockchain;

public interface IBlockRepository :IBaseRepository<Block>
{
    Task<IEnumerable<Block>> GetAll(CancellationToken cancellationToken);
    Task<Block> GetLastBlock(CancellationToken cancellationToken);
}

public class BlockRepository : GenericRepository<Block>, IBlockRepository
{
    public BlockRepository(ValidatorDbContext validatorDbContext) : base(validatorDbContext)
    {
    }

    public async Task<IEnumerable<Block>> GetAll(CancellationToken cancellationToken)
    {
       return await _validatorDbContext.Blocks.ToListAsync(cancellationToken);
    }

    public async Task<Block> GetLastBlock(CancellationToken cancellationToken)
    {
        return await _validatorDbContext.Blocks.OrderBy(a => a.BlockId).LastOrDefaultAsync(cancellationToken);
    }
}
