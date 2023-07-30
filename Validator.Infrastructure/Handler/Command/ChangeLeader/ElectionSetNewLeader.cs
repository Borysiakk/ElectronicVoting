using MediatR;
using Validator.Infrastructure.Service.ChangeLeader;

namespace Validator.Infrastructure.Handler.Command.ChangeLeader
{
    public class SetNewLeader :IRequest
    {
        public Int64 LeaderApproverId { get; set; }
        public string ElectionChangeLeaderId { get; set; }
    }

    public class SetNewLeaderHandler : IRequestHandler<SetNewLeader>
    {
        private readonly ILocalVoteChangeLeaderService _electionLocalVoteService;

        public SetNewLeaderHandler(ILocalVoteChangeLeaderService electionLocalVoteService)
        {
            _electionLocalVoteService = electionLocalVoteService;
        }

        public async Task Handle(SetNewLeader request, CancellationToken cancellationToken)
        {
            Console.WriteLine("Został wybrany nowy lider o Id {0}", request.LeaderApproverId);
        }
    }
}
