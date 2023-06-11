using ElectronicVoting.Persistence;
using ElectronicVoting.Validator.Domain.Table.BlockChain;
using ElectronicVoting.Validator.Infrastructure.Helper;

namespace ElectronicVoting.Infrastructure.Services;
public interface IBlochchainService
{
    public Task SaveBlock(Block block, CancellationToken cancellationToken);
}

public class BlochchainService : IBlochchainService
{
    private readonly ValidatorDbContext _applicationDbContext;

    public BlochchainService(ValidatorDbContext applicationDbContext)
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

