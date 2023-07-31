using MediatR;
using Validator.Domain.Table.Election;
using Validator.Infrastructure.Repository.Election;

namespace Validator.Infrastructure.Handler.Command.Election;
public class FinalizeLocalVoting : IRequest
{
    public byte[] Hash { get; set; }
    public string VoteProcessId { get; set; }
}

public class FinalizeLocalVotingHandler : IRequestHandler<FinalizeLocalVoting>
{
    private readonly IPendingLocalVoteRepository _pendingLocalVoteRepository;
    public FinalizeLocalVotingHandler(IPendingLocalVoteRepository pendingLocalVoteRepository)
    {
        _pendingLocalVoteRepository = pendingLocalVoteRepository;
    }

    public async Task Handle(FinalizeLocalVoting request, CancellationToken cancellationToken)
    {
        var pendingLocalVote = new PendingLocalVote()
        {
            Hash = request.Hash,
            VoteProcessId = request.VoteProcessId,
        };

        await _pendingLocalVoteRepository.Add(pendingLocalVote, cancellationToken);

        Console.WriteLine("FinalizeVoting");
    }
}
