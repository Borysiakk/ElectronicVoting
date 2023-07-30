using Validator.Domain.Table.ChangeLeader;
using Validator.Infrastructure.Repository;
using Validator.Infrastructure.Repository.ChangeLeader;
using Validator.Infrastructure.Service.ChangeLeader.Interface;

namespace Validator.Infrastructure.Service.ChangeLeader;

public interface ILocalVoteChangeLeaderService : IVoteChangeLeaderService
{
    Task<LocalVoteChangeLeader> Create(string electionId, CancellationToken cancellationToken);
}

public class LocalVoteChangeLeaderService : ILocalVoteChangeLeaderService
{
    private readonly ILeaderService _leaderService;
    private readonly IApproverService _approverService;
    private readonly ISettingRepository _settingRepository;
    private readonly ILocalVoteChangeLeaderRepository _localVoteChangeLeaderRepository;

    public LocalVoteChangeLeaderService(ILeaderService leaderService, IApproverService approverService, ISettingRepository settingRepository, ILocalVoteChangeLeaderRepository localVoteChangeLeaderRepository)
    {
        _leaderService = leaderService;
        _approverService = approverService;
        _settingRepository = settingRepository;
        _localVoteChangeLeaderRepository = localVoteChangeLeaderRepository;
    }

    public async Task<LocalVoteChangeLeader> Create(string electionChangeLeaderId, CancellationToken cancellationToken)
    {
        var nextLeaderId = await _leaderService.GetNextApproverId(cancellationToken);
        var meApprover = await _approverService.GetMyApprover(cancellationToken);
        if (meApprover == null)
            throw new Exception("Not found your Approver");

        return new LocalVoteChangeLeader()
        {
            Vote = nextLeaderId,
            ApproverId = meApprover.ApproverId,
            ElectionChangeLeaderId = electionChangeLeaderId
        };
    }

    public async Task<bool> IsVoteCountGreaterThanThreshold(string electionId, Int64 vote,CancellationToken cancellationToken)
    {
        var setting = await _settingRepository.Get("Approver", "AcceptableValidatorsCount", cancellationToken);
        if (setting == null)
            throw new ArgumentNullException("The specified settings were not found.");

        var acceptableCount = Int64.Parse(setting.Value);

        var countVote = await _localVoteChangeLeaderRepository.GetCountByIdAndVote(electionId, vote, cancellationToken);

        if (countVote >= acceptableCount)
            return true;

        return false;
    }
}

