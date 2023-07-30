using MediatR;
using Validator.Domain;
using Validator.Infrastructure.Service;
using Validator.Infrastructure.Repository.ChangeLeader;
using Validator.Infrastructure.Service.ChangeLeader;

namespace Validator.Infrastructure.Handler.Command.ChangeLeader;

public class PreElectionPreparation : IRequest
{
    public string PreElectionChangeLeaderId { get; set; }

}

public class PreElectionPreparationHandler : IRequestHandler<PreElectionPreparation>
{

    private readonly IApproverService _approverService;
    private readonly IPreChangeLeaderService _preElectionService;
    private readonly IPreLocalVoteChangeLeaderService _preElectionLocalVoteService;
    private readonly IPreLocalVoteChangeLeaderRepository _preElectionLocalVotesRepository;

    public PreElectionPreparationHandler(IApproverService approverService, IPreChangeLeaderService preElectionService, IPreLocalVoteChangeLeaderService preElectionLocalVoteService, IPreLocalVoteChangeLeaderRepository preElectionLocalVotesRepository)
    {
        _approverService = approverService;
        _preElectionService = preElectionService;
        _preElectionLocalVoteService = preElectionLocalVoteService;
        _preElectionLocalVotesRepository = preElectionLocalVotesRepository;
    }

    public async Task Handle(PreElectionPreparation request, CancellationToken cancellationToken)
    {

        var includeSender = false;
        var preElectionChangeLeaderEntity = await _preElectionLocalVoteService.Create(request.PreElectionChangeLeaderId, cancellationToken);
        await _preElectionLocalVotesRepository.Add(preElectionChangeLeaderEntity, cancellationToken);

        var preElectionVoteRecord = new PreElectionVoteRecordChangeLeader
        {
            Decision = preElectionChangeLeaderEntity.Decision,
            ApproverId = preElectionChangeLeaderEntity.ApproverId,
            PreElectionChangeLeaderId = request.PreElectionChangeLeaderId
        };
        await _approverService.SendPostToApprovers(Routes.PreElectionVoteRecord, preElectionVoteRecord, includeSender, cancellationToken);
    }
}