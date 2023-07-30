using Validator.Domain.Table;
using Validator.Infrastructure.Helper;
using Validator.Infrastructure.Repository;
using Validator.Infrastructure.Service.ChangeLeader;

namespace Validator.Infrastructure.Service;


public interface IApproverService

{
    Task<Approver?> GetMyApprover(CancellationToken cancellationToken);
    Task<IEnumerable<Approver>> GetAllWithoutMe(CancellationToken cancellationToken);

    Task SendPostToLeaderApprover<T>(string relativeUrl, T request, CancellationToken cancellationToken) where T : class;
    Task SendPostToApprovers<T>(string relativeUrl, T request, bool includeSender, CancellationToken cancellationToken) where T : class;
}
public class ApproverService : IApproverService
{
    private readonly ILeaderService _leaderService;
    private readonly IApproverRepository _approverRepository;

    public ApproverService(ILeaderService leaderService, IApproverRepository approverRepository)
    {
        _leaderService = leaderService;
        _approverRepository = approverRepository;
    }

    public async Task SendPostToApprovers<T>(string relativeUrl, T request, bool includeSender ,CancellationToken cancellationToken) where T: class
    {
        var approvers = includeSender == false
        ? await this.GetAllWithoutMe(cancellationToken)
        : await _approverRepository.GetAll(cancellationToken);

        foreach (var approver in approvers)
            await HttpHelper.PostAsync<T>(approver.NetworkAddress, relativeUrl, request, cancellationToken);
    }

    public async Task<IEnumerable<Approver>> GetAllWithoutMe(CancellationToken cancellationToken)
    {
        var myApproverName = Environment.GetEnvironmentVariable("CONTAINER_NAME");
        if (myApproverName == null)
            throw new Exception("Error reading your container name");

        return await _approverRepository.GetAllWithout(myApproverName, cancellationToken);
    }

    public async Task<Approver?> GetMyApprover(CancellationToken cancellationToken)
    {
        var myApproverName = Environment.GetEnvironmentVariable("CONTAINER_NAME");
        if (myApproverName == null)
            throw new Exception("Error reading your container name");

        return await _approverRepository.GetByName(myApproverName, cancellationToken);
    }

    public async Task SendPostToLeaderApprover<T>(string relativeUrl, T request, CancellationToken cancellationToken) where T : class
    {
        var approverLeader = await _leaderService.GetCurrentApproverLeader(cancellationToken);

        await HttpHelper.PostAsync<T>(approverLeader.NetworkAddress, relativeUrl, request, cancellationToken);
    }
}
