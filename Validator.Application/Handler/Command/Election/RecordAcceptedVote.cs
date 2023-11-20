using MediatR;
using Validator.Domain.Table.Electronic;
using Validator.Infrastructure.Repository.Election;

namespace Validator.Application.Handler.Command.Election;

public record RecordAcceptedVote :IRequest
{
    public byte[] Hash { get; set; }
    public bool ResultVerifyVote { get; set; }
    public string SessionElectionId { get; set; }

    public RecordAcceptedVote(byte[] hash, bool resultVerifyVote, string sessionElectionId)
    {
        Hash = hash;
        ResultVerifyVote = resultVerifyVote;
        SessionElectionId = sessionElectionId;
    }
}


public class RecordAcceptedVoteHandler : IRequestHandler<RecordAcceptedVote>
{
    private readonly IVoteRecordRepository _voteRecordRepository;
    private readonly IVoteConfirmedRepository _voteConfirmedRepository;

    public RecordAcceptedVoteHandler(IVoteConfirmedRepository voteConfirmedRepository, IVoteRecordRepository voteRecordRepository)
    {
        _voteRecordRepository = voteRecordRepository;
        _voteConfirmedRepository = voteConfirmedRepository;
    }

    public async Task Handle(RecordAcceptedVote request, CancellationToken cancellationToken)
    {
        var registerVote = await _voteRecordRepository.GetByVoteProcessId(request.SessionElectionId, cancellationToken);
        var voteConfirmed = new VoteConfirmed(registerVote.Vote, request.SessionElectionId);

        await _voteConfirmedRepository.Add(voteConfirmed, cancellationToken);


    }
}
