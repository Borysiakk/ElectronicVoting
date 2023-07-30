using MediatR;
using Validator.Domain.Table.ChangeLeader;
using Validator.Infrastructure.Repository.ChangeLeader;

namespace Validator.Infrastructure.Handler.Command.ChangeLeader
{
    public class PreElectionChangeLeaderNotifyLeaderCompleted : IRequest
    {
        public bool Decision { get; set; }
        public Int64 ApproverId {get;set;}
        public string PreElectionChangeLeaderId { get; set; }
    }

    public class PreElectionChangeLeaderNotifyLeaderCompletedHandler : IRequestHandler<PreElectionChangeLeaderNotifyLeaderCompleted>
    {
        private readonly IPreLeaderVoteChangeLeaderRepository _preElectionLeaderVoteChangeLeaderRepository;

        public PreElectionChangeLeaderNotifyLeaderCompletedHandler(IPreLeaderVoteChangeLeaderRepository preElectionLeaderVoteChangeLeaderRepository)
        {
            _preElectionLeaderVoteChangeLeaderRepository = preElectionLeaderVoteChangeLeaderRepository;
        }

        public async Task Handle(PreElectionChangeLeaderNotifyLeaderCompleted request, CancellationToken cancellationToken)
        {
            var preElectionLeaderVote = new PreLeaderVoteChangeLeader()
            {
                Decision = request.Decision,
                ApproverId = request.ApproverId,
                PreElectionChangeLeaderId = request.PreElectionChangeLeaderId
            };

            await _preElectionLeaderVoteChangeLeaderRepository.Add(preElectionLeaderVote, cancellationToken);
        }
    }
}
