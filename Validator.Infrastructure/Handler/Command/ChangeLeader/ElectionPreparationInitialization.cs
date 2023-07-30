using MediatR;
using Validator.Domain;
using Validator.Infrastructure.Service;
using Validator.Infrastructure.Service.ChangeLeader;

namespace Validator.Infrastructure.Handler.Command.ChangeLeader
{
    public class ElectionPreparationInitChangeLeader : IRequest
    {
    }

    public class ElectionPreparationInitChangeLeaderHandler : IRequestHandler<ElectionPreparationInitChangeLeader>
    {
        private readonly ILeaderService _leaderService;
        private readonly IApproverService _approverService;

        public ElectionPreparationInitChangeLeaderHandler(ILeaderService leaderService, IApproverService approverService)
        {
            _leaderService = leaderService;
            _approverService = approverService;
        }

        public async Task Handle(ElectionPreparationInitChangeLeader request, CancellationToken cancellationToken)
        {
            bool includeSender = true;
            var electionPreparation = new ElectionPreparationChangeLeader
            {
                ElectionChangeLeaderId = Guid.NewGuid().ToString(),
            };

            await _approverService.SendPostToApprovers(Routes.ElectionPreparation, electionPreparation, includeSender, cancellationToken);
        }
    }
}
