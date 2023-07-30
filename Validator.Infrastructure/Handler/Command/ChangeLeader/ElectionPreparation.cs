using MediatR;
using Validator.Domain;
using Validator.Infrastructure.Service;
using Validator.Infrastructure.Service.ChangeLeader;

namespace Validator.Infrastructure.Handler.Command.ChangeLeader
{
    public class ElectionPreparationChangeLeader : IRequest
    {
        public string ElectionChangeLeaderId { get; set; }
    }


    public class ElectionPreparationChangeLeaderHandler : IRequestHandler<ElectionPreparationChangeLeader>
    {
        private readonly ILeaderService _leaderService;
        private readonly IApproverService _approverService;

        public ElectionPreparationChangeLeaderHandler(ILeaderService leaderService, IApproverService approverService)
        {
            _leaderService = leaderService;
            _approverService = approverService;
        }

        public async Task Handle(ElectionPreparationChangeLeader request, CancellationToken cancellationToken)
        {
            var includeSender = true;
            var vote = await _leaderService.GetNextApproverId(cancellationToken);
            var electionVoteRecord = new ElectionVoteRecordChangeLeader()
            {
                ProposedLeaderApproverId = vote,
                ElectionChangeLeaderId = request.ElectionChangeLeaderId,
            };

            await _approverService.SendPostToApprovers(Routes.ElectionVoteRecord, electionVoteRecord, includeSender, cancellationToken);
        }
    }
}
