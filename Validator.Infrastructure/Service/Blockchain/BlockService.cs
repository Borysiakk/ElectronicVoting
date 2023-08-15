using Validator.Domain.Table;
using Validator.Infrastructure.Repository.Blockchain;

namespace Validator.Infrastructure.Service.Blockchain;

public interface IBlockService
{
    public Task<Block> Create(CancellationToken cancellationToken);
}

public class BlockService : IBlockService
{
    private readonly IBlockRepository _blockRepository;

    public BlockService(IBlockRepository blockRepository)
    {
        _blockRepository = blockRepository;
    }

    public async Task<Block> Create(CancellationToken cancellationToken)
    {
        var lastBlock = await _blockRepository.GetLastBlock(cancellationToken);
        if( lastBlock == null)
            return new Block();

        return new Block(lastBlock.Hash); ;
    }
}
