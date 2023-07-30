using MediatR;
using Validator.Infrastructure.Handler.Command.Election.PbftConsesus;
using Validator.Infrastructure.Hangfire;
using Validator.Infrastructure.Service.Election;

namespace Validator.Infrastructure.Handler.Command.Election;

public class InitiateVotingProcess : IRequest
{

}

public class InitiateVotingProcessHandler : IRequestHandler<InitiateVotingProcess>
{
    private readonly IVoteRecordService _voteRecordService;
    private readonly IBackgroundJobMediatorClient _backgroundJobMediatorClient;

    public InitiateVotingProcessHandler(IVoteRecordService voteRecordService, IBackgroundJobMediatorClient backgroundJobMediatorClient)
    {
        _voteRecordService = voteRecordService;
        _backgroundJobMediatorClient = backgroundJobMediatorClient;
    }

    public async Task Handle(InitiateVotingProcess request, CancellationToken cancellationToken)
    {
        var voteProcessId = Guid.NewGuid().ToString();

        await _voteRecordService.PublishVote(voteProcessId, cancellationToken);

        _backgroundJobMediatorClient.Enqueue(new PrePrepareElection());
    }
}
