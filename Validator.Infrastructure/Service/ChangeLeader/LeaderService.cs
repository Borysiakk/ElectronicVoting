using Validator.Domain.Table;
using Validator.Infrastructure.Repository;

namespace Validator.Infrastructure.Service.ChangeLeader;

public interface ILeaderService
{
    Task<Int64> GetNextApproverId(CancellationToken cancellationToken);
    Task<Approver> GetCurrentApproverLeader(CancellationToken cancellationToken);
}

public class LeaderService : ILeaderService
{
    private readonly ILeaderRepository _leaderRepository;
    private readonly IApproverRepository _approverRepository;

    public LeaderService(ILeaderRepository leaderRepository, IApproverRepository approverRepository)
    {
        _leaderRepository = leaderRepository;
        _approverRepository = approverRepository;
    }

    public async Task<Approver> GetCurrentApproverLeader(CancellationToken cancellationToken)
    {
        var approverId = await _leaderRepository.GetApproverIdForLatestLeader(cancellationToken);
        var approverLeader = await _approverRepository.GetbyId(approverId, cancellationToken);

        if (approverLeader == null)
            throw new Exception("Leader not found!");

        return approverLeader;
    }

    public async Task<Int64> GetNextApproverId(CancellationToken cancellationToken)
    {
        var lastApproverId = await _approverRepository.GetLastId(cancellationToken);
        var currentApproverId = await _leaderRepository.GetApproverIdForLatestLeader(cancellationToken);

        currentApproverId = currentApproverId + 1;
        if(currentApproverId > lastApproverId)
            return 1;

        return currentApproverId;
    }
}
