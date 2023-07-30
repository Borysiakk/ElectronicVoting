using MediatR;
using Validator.Domain.Table.ChangeLeader;
using Validator.Infrastructure.Repository.ChangeLeader;

namespace Validator.Infrastructure.Handler.Command.ChangeLeader;
public class ElectionChangeLeaderNotifyLeaderCompleted : IRequest
{
    public Int64 Vote { get; set; }
    public Int64 ApproverId { get; set; }
    public string ElectionChangeLeaderId { get; set; }
}

public class ElectionChangeLeaderNotifyLeaderCompletedHandler : IRequestHandler<ElectionChangeLeaderNotifyLeaderCompleted>
{
    private readonly ILeaderVoteChangeLeaderRepository _leaderVoteChangeLeaderRepository;

    public ElectionChangeLeaderNotifyLeaderCompletedHandler(ILeaderVoteChangeLeaderRepository leaderVoteChangeLeaderRepository)
    {
        _leaderVoteChangeLeaderRepository = leaderVoteChangeLeaderRepository;
    }

    public async Task Handle(ElectionChangeLeaderNotifyLeaderCompleted request, CancellationToken cancellationToken)
    {
        var electionLeaderVoteChangeLeader = new LeaderVoteChangeLeader()
        {
            Vote = request.Vote,
            ApproverId = request.ApproverId,
            ElectionChangeLeaderId = request.ElectionChangeLeaderId
        };

        await _leaderVoteChangeLeaderRepository.Add(electionLeaderVoteChangeLeader, cancellationToken);
    }
}
