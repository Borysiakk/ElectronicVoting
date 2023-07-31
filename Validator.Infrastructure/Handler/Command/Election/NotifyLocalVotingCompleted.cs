using MediatR;
using Validator.Domain.Table.Election;
using Validator.Infrastructure.Repository.Election;

namespace Validator.Infrastructure.Handler.Command.Election;

public class NotifyLocalVotingCompleted :IRequest
{
    public byte[] Hash { get; set; }
    public string VoteProcessId { get; set; }
}

public class NotifyLocalVotingCompletedHandler : IRequestHandler<NotifyLocalVotingCompleted>
{
    private readonly IPendingLeaderVoteRepository _pendingLeaderVoteRepository;

    public NotifyLocalVotingCompletedHandler(IPendingLeaderVoteRepository pendingLeaderVoteRepository)
    {
        _pendingLeaderVoteRepository = pendingLeaderVoteRepository;
    }

    public async Task Handle(NotifyLocalVotingCompleted request, CancellationToken cancellationToken)
    {
        var pendingLeaderVote = new PendingLeaderVote()
        {
            Hash = request.Hash,
            VoteProcessId = request.VoteProcessId,
        };

        await _pendingLeaderVoteRepository.Add(pendingLeaderVote, cancellationToken);
    }
}
