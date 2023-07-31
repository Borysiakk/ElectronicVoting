using MediatR;
using Validator.Domain;
using Validator.Infrastructure.Service;
using Validator.Infrastructure.Service.Election;

namespace Validator.Infrastructure.Handler.Command.Election;

public class InitiateVotingProcess : IRequest
{
    public Int64 Vote { get; set; }
}

public class InitiateVotingProcessHandler : IRequestHandler<InitiateVotingProcess>
{
    private readonly IApproverService _approverService;
    private readonly IVoteRecordService _voteRecordService;

    public InitiateVotingProcessHandler(IApproverService approverService, IVoteRecordService voteRecordService)
    {
        _approverService = approverService;
        _voteRecordService = voteRecordService;
    }

    public async Task Handle(InitiateVotingProcess request, CancellationToken cancellationToken)
    {
        var includeSender = true;

        var voteProcessId = Guid.NewGuid().ToString();
        var validateVotes = new ValidateLocalVote(request.Vote, voteProcessId);

        await _voteRecordService.PublishVote(voteProcessId, cancellationToken);
        await _approverService.SendPostToApprovers(Routes.ValidateLocalVote, validateVotes, includeSender, cancellationToken);
    }
}
