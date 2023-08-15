using MediatR;
using Validator.Domain.Table.Election;
using Validator.Infrastructure.Repository.Election;

namespace Validator.Infrastructure.Handler.Command.Election;

public class RecordAcceptedVote :IRequest
{
    public Int64 Vote { get; set; }
    public byte[] Hash { get; set; }
    public string VoteProcessId { get; set; }

    public RecordAcceptedVote(Int64 vote, string voteProcessId, byte[] hash)
    {
        Vote = vote;
        Hash = hash;
        VoteProcessId = voteProcessId;
    }
}

public class RecordAcceptedVoteHandler : IRequestHandler<RecordAcceptedVote>
{
    private readonly IVoteRecordRepository _voteRecordRepository;
    private readonly IVoteConfirmedRepository _voteConfirmedRepository;

    public RecordAcceptedVoteHandler(IVoteRecordRepository voteRecordRepository, IVoteConfirmedRepository voteConfirmedRepository)
    {
        _voteRecordRepository = voteRecordRepository;
        _voteConfirmedRepository = voteConfirmedRepository;
    }

    public async Task Handle(RecordAcceptedVote request, CancellationToken cancellationToken)
    {
        var voteConfirmed = new VoteConfirmed(request.Vote, request.Hash, request.VoteProcessId);

        await _voteConfirmedRepository.Add(voteConfirmed,cancellationToken);
    }
}
