using MediatR;
using Validator.Domain.Table;
using Validator.Infrastructure.Repository.Blockchain;

namespace Validator.Infrastructure.Handler.Query.Blockchain;

public class GetBlocks : IRequest<IEnumerable<Block>>
{

}

public class GetBlocksHandler : IRequestHandler<GetBlocks, IEnumerable<Block>>
{
    private IBlockRepository _blockRepository;

    public GetBlocksHandler(IBlockRepository blockRepository)
    {
        _blockRepository = blockRepository;
    }

    public async Task<IEnumerable<Block>> Handle(GetBlocks request, CancellationToken cancellationToken)
    {
        return await _blockRepository.GetAll(cancellationToken);
    }
}