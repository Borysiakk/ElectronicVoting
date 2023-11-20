using MediatR;
using Validator.Domain.Table.Electronic;
using Validator.Infrastructure.Repository.Election;

namespace Validator.Application.Handler.Command.Election;

public record RegisterVote :IRequest
{
    public string Vote { get; set; }
    public string SessionElectionId { get; set; }

    public RegisterVote() { }
    public RegisterVote(string vote ,string sessionElectionId)
    {
        Vote = vote;
        SessionElectionId = sessionElectionId;
    }
}

public class RegisterVoteHandler : IRequestHandler<RegisterVote>
{
    private readonly IVoteRecordRepository _voteRecordRepository;

    public RegisterVoteHandler(IVoteRecordRepository voteRecordRepository)
    {
        _voteRecordRepository = voteRecordRepository;
    }

    public async Task Handle(RegisterVote request, CancellationToken cancellationToken)
    {
        var voteRecord = new VoteRecord(request.Vote ,request.SessionElectionId);
        await _voteRecordRepository.Add(voteRecord, cancellationToken);
    }

}
