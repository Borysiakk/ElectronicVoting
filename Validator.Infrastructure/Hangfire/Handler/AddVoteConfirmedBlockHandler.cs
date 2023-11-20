using MediatR;
using Validator.Domain.Table.Blockchain;
using Validator.Infrastructure.Helper;
using Validator.Infrastructure.Repository.Blockchain;
using Validator.Infrastructure.Repository.Election;

namespace Validator.Infrastructure.Hangfire.Handler;


public class AddVoteConfirmedBlock :IRequest
{

}

public class AddVoteConfirmedBlockHandler : IRequestHandler<AddVoteConfirmedBlock>
{
    private readonly IBlockRepository _blockRepository;
    private readonly IVoteConfirmedRepository _voteConfirmedRepository;

    public AddVoteConfirmedBlockHandler(IBlockRepository blockRepository, IVoteConfirmedRepository voteConfirmedRepository)
    {
        _blockRepository = blockRepository;
        _voteConfirmedRepository = voteConfirmedRepository;
    }

    public async Task Handle(AddVoteConfirmedBlock request, CancellationToken cancellationToken)
    {
        var votes = await _voteConfirmedRepository.GetAndUpdateByInInserted(cancellationToken);

        if (votes.Count == 0)
            return;

        var lastBlock = await _blockRepository.GetLastBlock(cancellationToken);
        var transactions = votes.Select(t => new Transaction()
        {
            Vote = t.Vote,
        }).ToList();

        Block block = new Block(lastBlock.Hash);
        block.Transactions = transactions;
        block.Hash = HashHelper.ComputeHash(block);

        await _blockRepository.Add(block, cancellationToken);
    }
}
