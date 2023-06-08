using ElectronicVoting.Domain.Table.Blockchain;
using ElectronicVoting.Persistence;
using ElectronicVoting.Infrastructure.Helper;

namespace ElectronicVoting.Infrastructure.Services
{
    public interface IBlochchainService
    {
        public Task SaveBlock(Block block, CancellationToken cancellationToken);
    }

    public class BlochchainService : IBlochchainService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public BlochchainService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task SaveBlock(Block block, CancellationToken cancellationToken)
        {
            block.Hash = block.CalculateHash();

            await _applicationDbContext.Blocks.AddAsync(block, cancellationToken);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
