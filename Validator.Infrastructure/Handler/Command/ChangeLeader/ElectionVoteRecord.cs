using MediatR;
using Validator.Infrastructure.Repository.ChangeLeader;
using Validator.Infrastructure.Service.ChangeLeader;

namespace Validator.Infrastructure.Handler.Command.ChangeLeader
{
    public class ElectionVoteRecordChangeLeader : IRequest
    {
        public string ElectionChangeLeaderId { get; set; }
        public Int64 ProposedLeaderApproverId { get; set; }
    }

    public class ElectionVoteRecordChangeLeaderHandler : IRequestHandler<ElectionVoteRecordChangeLeader>
    {
        private readonly ILocalVoteChangeLeaderService _electionLocalVoteService;
        private readonly ILocalVoteChangeLeaderRepository _electionLocalVoteRepository;

        public ElectionVoteRecordChangeLeaderHandler(ILocalVoteChangeLeaderService electionLocalVoteService, ILocalVoteChangeLeaderRepository electionLocalVoteRepository)
        {
            _electionLocalVoteService = electionLocalVoteService;
            _electionLocalVoteRepository = electionLocalVoteRepository;
        }

        public async Task Handle(ElectionVoteRecordChangeLeader request, CancellationToken cancellationToken)
        {
            var preElectionChangeLeaderEntity = await _electionLocalVoteService.Create(request.ElectionChangeLeaderId, cancellationToken);

            await _electionLocalVoteRepository.Add(preElectionChangeLeaderEntity, cancellationToken);
        }
    }
}
