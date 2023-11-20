using MediatR;
using Validator.Application.Service;
using Validator.Domain.Table.Electronic;
using Validator.Infrastructure.Repository.Election;

namespace Validator.Application.Handler.Command.Election;

public record NotifyLocalVoteVerificationCompleted :IRequest
{
    public byte[] Hash { get; set; }
    public bool ResultVerifyVote { get; set; }
    public string SessionElectionId { get; set; }

    public NotifyLocalVoteVerificationCompleted(byte[] hash, bool resultVerifyVote, string sessionElectionId)
    {
        Hash = hash;
        ResultVerifyVote = resultVerifyVote;
        SessionElectionId = sessionElectionId;
    }
}


public class NotifyLocalVoteVerificationCompletedHandler :IRequestHandler<NotifyLocalVoteVerificationCompleted>
{
    private readonly IPendingVoteService _pendingVoteService;
    private readonly IPendingLeaderVoteRepository _pendingLeaderVoteRepository;

    public NotifyLocalVoteVerificationCompletedHandler(IPendingVoteService pendingVoteService, IPendingLeaderVoteRepository pendingLeaderVoteRepository)
    {
        _pendingVoteService = pendingVoteService;
        _pendingLeaderVoteRepository = pendingLeaderVoteRepository;
    }

    public async Task Handle(NotifyLocalVoteVerificationCompleted request, CancellationToken cancellationToken)
    {
        var pendingLeaderVote = new PendingLeaderVote()
        {
            Hash = request.Hash,
            ResultVerifyVote = request.ResultVerifyVote,
            SessionElectionId = request.SessionElectionId,
        };

        await _pendingLeaderVoteRepository.Add(pendingLeaderVote, cancellationToken);
        await _pendingVoteService.CheckAndNotifyVoteLeaderCompletion(request.SessionElectionId, request.ResultVerifyVote, request.Hash, cancellationToken);
    }
}
