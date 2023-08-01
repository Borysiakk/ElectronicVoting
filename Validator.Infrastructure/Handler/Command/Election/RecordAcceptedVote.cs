using MediatR;
using System.Linq;
using Validator.Domain.Table.Election;
using Validator.Infrastructure.Repository.Election;

namespace Validator.Infrastructure.Handler.Command.Election;

public class RecordAcceptedVote :IRequest
{
    public byte[] Hash { get; set; }
    public string VoteProcessId { get; set; }

    public RecordAcceptedVote(string voteProcessId, byte[] hash)
    {
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
        var voteConfirmeds = await _voteConfirmedRepository.GetVoteConfirmationsInInsertionOrder(cancellationToken);
        
    }
}
