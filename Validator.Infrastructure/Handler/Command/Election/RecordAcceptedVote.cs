using MediatR;

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
    public async Task Handle(RecordAcceptedVote request, CancellationToken cancellationToken)
    {
        Console.WriteLine("Głos został potwierdzony przez wszystkich i został dodany do wyniku, brawo");
    }
}
