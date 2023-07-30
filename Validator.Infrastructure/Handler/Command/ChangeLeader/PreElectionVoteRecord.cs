using MediatR;
using Validator.Infrastructure.Repository.ChangeLeader;
using Validator.Infrastructure.Service.ChangeLeader;

namespace Validator.Infrastructure.Handler.Command.ChangeLeader
{
    public class PreElectionVoteRecordChangeLeader : IRequest
    {
        public bool Decision { get; set; }
        public Int64 ApproverId { get; set; }
        public string PreElectionChangeLeaderId { get; set; }

    }

    public class PreElectionVoteRecordChangeLeaderHandler : IRequestHandler<PreElectionVoteRecordChangeLeader>
    {
        private readonly IPreLocalVoteChangeLeaderService _preElectionLocalVoteService;
        private readonly IPreLocalVoteChangeLeaderRepository _preElectionLocalVotesRepository;

        public PreElectionVoteRecordChangeLeaderHandler(IPreLocalVoteChangeLeaderService preElectionLocalVoteService, IPreLocalVoteChangeLeaderRepository preElectionLocalVotesRepository)
        {
            _preElectionLocalVoteService = preElectionLocalVoteService;
            _preElectionLocalVotesRepository = preElectionLocalVotesRepository;
        }

        public async Task Handle(PreElectionVoteRecordChangeLeader request, CancellationToken cancellationToken)
        {
            var preElectionChangeLeaderEntity = await _preElectionLocalVoteService.Create(request.PreElectionChangeLeaderId, cancellationToken);

            await _preElectionLocalVotesRepository.Add(preElectionChangeLeaderEntity, cancellationToken);
        }
    }
}
