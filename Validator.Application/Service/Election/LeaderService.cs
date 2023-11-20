using Validator.Domain.Table.Electronic;
using Validator.Infrastructure.Repository;

namespace Validator.Application.Service.Election;

public interface ILeaderService
{
    Task<long> GetNextApproverId(CancellationToken cancellationToken);
    Task<Approver> GetCurrentApproverLeader(CancellationToken cancellationToken);
}

public sealed class LeaderService : ILeaderService
{
    private readonly ILeaderRepository _leaderRepository;
    private readonly IApproverRepository _approverRepository;

    public LeaderService(IApproverRepository approverRepository, ILeaderRepository leaderRepository)
    {
        _approverRepository = approverRepository;
        _leaderRepository = leaderRepository;
    }

    public async Task<long> GetNextApproverId(CancellationToken cancellationToken)
    {
        var lastApproverId = await _approverRepository.GetLastId(cancellationToken);
        var currentApproverId = await _leaderRepository.GetApproverIdForLatestLeader(cancellationToken);

        currentApproverId = currentApproverId + 1;
        return currentApproverId > lastApproverId ? 1 : currentApproverId;
    }

    public async Task<Approver> GetCurrentApproverLeader(CancellationToken cancellationToken)
    {
        var approverId = await _leaderRepository.GetApproverIdForLatestLeader(cancellationToken);
        var approverLeader = await _approverRepository.GetById(approverId, cancellationToken);

        return approverLeader ?? throw new Exception("Leader not found!");
    }
}
