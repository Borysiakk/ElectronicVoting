using Validator.Domain.Table.Electronic;
using Validator.Infrastructure.Repository;
using Validator.Infrastructure.RestSharp;

namespace Validator.Application.Service.Election;

public interface IApproverService
{
    Task<Approver> GetMyApprover(CancellationToken cancellationToken);
    Task<IEnumerable<Approver>> GetAllWithoutMe(CancellationToken cancellationToken);

    Task SendPostToApprovers<T>(string relativeUrl, T request, bool includeSender, CancellationToken cancellationToken) where T : class;

    Task SendPostToLeaderApprover<T>(string relativeUrl, T request, CancellationToken cancellationToken) where T : class;
}

public sealed class ApproverService : IApproverService
{
    private readonly ILeaderService _leaderService;
    private readonly IRestCommunicator _restCommunicator;
    private readonly IApproverRepository _approverRepository;

    public ApproverService(IApproverRepository approverRepository, ILeaderService leaderService, IRestCommunicator restCommunicator)
    {
        _leaderService = leaderService;
        _restCommunicator = restCommunicator;
        _approverRepository = approverRepository;
    }

    public async Task<Approver> GetMyApprover(CancellationToken cancellationToken)
    {
        var myApproverName = Environment.GetEnvironmentVariable("COMPUTER_NAME");

        if (myApproverName == null)
            throw new Exception("Error reading your container name");

        return await _approverRepository.GetByName(myApproverName, cancellationToken);
    }

    public async Task<IEnumerable<Approver>> GetAllWithoutMe(CancellationToken cancellationToken)
    {
        var myApproverName = Environment.GetEnvironmentVariable("CONTAINER_NAME");
        if (myApproverName == null)
            throw new Exception("Error reading your container name");

        return await _approverRepository.GetAllWithoutMe(myApproverName, cancellationToken);
    }

    public async Task SendPostToApprovers<T>(string relativeUrl, T request, bool includeSender,
        CancellationToken cancellationToken) where T : class
    {
        var approvers = includeSender == false
            ? await GetAllWithoutMe(cancellationToken)
            : await _approverRepository.GetAll(cancellationToken);

        foreach (var approver in approvers)
        {
            var r = await _restCommunicator.PostAsync(approver.Address, relativeUrl, request, cancellationToken);
        }
    }

    public async Task SendPostToLeaderApprover<T>(string relativeUrl, T request, CancellationToken cancellationToken)
        where T : class
    {
        var approverLeader = await _leaderService.GetCurrentApproverLeader(cancellationToken);
        await _restCommunicator.PostAsync(approverLeader.Address, relativeUrl, request, cancellationToken);
    }
}