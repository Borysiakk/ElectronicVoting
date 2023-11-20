using Microsoft.EntityFrameworkCore;
using Validator.Domain.Table.Blockchain;
using Validator.Infrastructure.EntityFramework;

namespace Validator.Infrastructure.Repository.Blockchain;

public interface IBlockRepository : IBaseRepository<Block>
{
    Task<IEnumerable<Block>> GetAll(CancellationToken cancellationToken);
    Task<Block> GetLastBlock(CancellationToken cancellationToken);
}

public class BlockRepository : GenericRepository<Block>, IBlockRepository
{
    public BlockRepository(ElectionDatabaseContext electionDatabaseContext) : base(electionDatabaseContext) {}

    public async Task<IEnumerable<Block>> GetAll(CancellationToken cancellationToken)
    {
        try
        {
            return await ElectionContext.Blocks.Include(a => a.Transactions).ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task<Block> GetLastBlock(CancellationToken cancellationToken)
    {
        return await ElectionContext.Blocks.OrderBy(a => a.BlockId).LastOrDefaultAsync(cancellationToken);
    }
}
