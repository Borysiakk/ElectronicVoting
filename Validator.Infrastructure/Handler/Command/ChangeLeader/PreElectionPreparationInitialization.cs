using MediatR;
using Validator.Domain;
using Validator.Infrastructure.Service;

namespace Validator.Infrastructure.Handler.Command.ChangeLeader;
public class PreElectionPreparationInitChangeLeader : IRequest
{

}

public class PreElectionPreparationInitChangeLeaderHandler : IRequestHandler<PreElectionPreparationInitChangeLeader>
{
    private readonly IApproverService _approverService;

    public PreElectionPreparationInitChangeLeaderHandler(IApproverService approverService)
    {
        _approverService = approverService;
    }

    public async Task Handle(PreElectionPreparationInitChangeLeader request, CancellationToken cancellationToken)
    {
        var includeSender = true;
        var preElectionId = Guid.NewGuid().ToString();
        var preElectionPreparation = new PreElectionPreparation
        {
            PreElectionChangeLeaderId = preElectionId
        };

        await _approverService.SendPostToApprovers(Routes.PreElectionPreparation, preElectionPreparation, includeSender, cancellationToken);
    }
}
