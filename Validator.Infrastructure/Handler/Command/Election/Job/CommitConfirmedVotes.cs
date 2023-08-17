using MediatR;
using Validator.Domain.Table;
using Validator.Infrastructure.Repository.Blockchain;
using Validator.Infrastructure.Repository.Election;
using Validator.Infrastructure.Service.Blockchain;

namespace Validator.Infrastructure.Handler.Command.Election.Job;

public class CommitConfirmedVotes :IRequest
{

}


public class CommitConfirmedVotesHandler : IRequestHandler<CommitConfirmedVotes>
{
    private readonly IBlockService _blockService;
    private readonly IBlockRepository _blockRepository;
    private readonly IVoteConfirmedRepository _voteConfirmedRepository;

    public CommitConfirmedVotesHandler(IBlockService blockService, IBlockRepository blockRepository, IVoteConfirmedRepository voteConfirmedRepository)
    {
        _blockService = blockService;
        _blockRepository = blockRepository;
        _voteConfirmedRepository = voteConfirmedRepository;
    }

    public async Task Handle(CommitConfirmedVotes request, CancellationToken cancellationToken)
    {
        Console.WriteLine("Zaczynamy wstawianie glosów do blockchain");

        var block = await _blockService.Create(cancellationToken);
        var confirmedVotes = await _voteConfirmedRepository.GetAndUpdateByInInserted(cancellationToken);

        confirmedVotes.ForEach(a => block.Transactions.Add(new Transaction(a.Vote)));

        await _blockRepository.Add(block, cancellationToken);
    }

    // When we add a new block its hash is calculated in the BeforeCreateBlockTrigger.cs trigger
}