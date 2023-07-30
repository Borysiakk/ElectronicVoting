using Validator.Domain.Table.ChangeLeader;
using Validator.Infrastructure.Repository;
using Validator.Infrastructure.Repository.ChangeLeader;
using Validator.Infrastructure.Service.ChangeLeader.Interface;

namespace Validator.Infrastructure.Service.ChangeLeader;

public interface IPreLocalVoteChangeLeaderService : IPreVoteChangeLeaderService
{
    Task<PreLocalVoteChangeLeader> Create(string preElectionId, CancellationToken cancellationToken);
}

public class PreLocalVoteChangeLeaderService : IPreLocalVoteChangeLeaderService
{
    private readonly IApproverService _approverService;
    private readonly ISettingRepository _settingRepository;
    private readonly IPreChangeLeaderService _preChangeLeaderService;
    private readonly IPreLocalVoteChangeLeaderRepository _preLocalVoteChangeLeaderRepository;

    public PreLocalVoteChangeLeaderService(IApproverService approverService, ISettingRepository settingRepository, IPreChangeLeaderService preChangeLeaderService, IPreLocalVoteChangeLeaderRepository preLocalVoteChangeLeaderRepository)
    {
        _approverService = approverService;
        _settingRepository = settingRepository;
        _preChangeLeaderService = preChangeLeaderService;
        _preLocalVoteChangeLeaderRepository = preLocalVoteChangeLeaderRepository;
    }

    public async Task<PreLocalVoteChangeLeader> Create(string preElectionId,CancellationToken cancellationToken)
    {
        var approver = await _approverService.GetMyApprover(cancellationToken);
        var decision = await _preChangeLeaderService.CheckLeadershipChangeReason(cancellationToken);

        var preElectionLocalVoteChangeLeader = new PreLocalVoteChangeLeader()
        {
            Decision = decision,
            ApproverId = approver.ApproverId,
            PreElectionChangeLeaderId = preElectionId
        };

        return preElectionLocalVoteChangeLeader;
    }

    public async Task<bool> IsVoteCountGreaterThanThreshold(string preElectionId, bool decision, CancellationToken cancellationToken)
    {
        var setting = await _settingRepository.Get("Approver", "AcceptableValidatorsCount", cancellationToken);
        if (setting == null)
            throw new ArgumentNullException("The specified settings were not found.");

        var acceptableCount = Int64.Parse(setting.Value);
        var countVote = await _preLocalVoteChangeLeaderRepository.GetCountByIdAndDecision(preElectionId, decision, cancellationToken);

        if( countVote >= acceptableCount)
            return true;

        return false;
    }
}
