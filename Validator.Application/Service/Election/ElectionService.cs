using Refit;
using Validator.Application.Handler.Command.Election;
using Validator.Application.RestApi.Election;
using Validator.Domain.Table.Electronic;
using Validator.Infrastructure.Cache;
using Validator.Infrastructure.Repository;

namespace Validator.Application.Service.Election;

public interface IElectionService
{
    Task RecordAcceptedVote(RecordAcceptedVote request, CancellationToken cancellationToken);
    Task RegisterVotes(RegisterVote request, CancellationToken cancellationToken);
    Task VerifyLocalVotes(VerifyLocalVote request, CancellationToken cancellationToken);
    Task FinalizeLocalVotes(FinalizeLocalVote request, CancellationToken cancellationToken);
    Task NotifyLocalVoteVerificationsCompleted(NotifyLocalVoteVerificationCompleted request, CancellationToken cancellationToken);
}

public class ElectionService : IElectionService
{
    private readonly ICacheService _cacheService;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IApproverRepository _approverRepository;

    public ElectionService(ICacheService cacheService, IHttpClientFactory httpClientFactory, IApproverRepository approverRepository)
    {
        _cacheService = cacheService;
        _httpClientFactory = httpClientFactory;
        _approverRepository = approverRepository;
    }

    public async Task VerifyLocalVotes(VerifyLocalVote request, CancellationToken cancellationToken)
    {
        var approvers = await _cacheService.GetOrSetCache<IEnumerable<Approver>>("Approver", "All", async() =>
        {
            return  await _approverRepository.GetAll(cancellationToken);
        }, new TimeSpan(24, 0, 0));

        var tasks = approvers.Select(app =>
        {
            var electionApiService = CreateElectionApiService(app.Address);
            return electionApiService.VerifyLocalVote(request);
        });

        await Task.WhenAll(tasks);
    }

    public async Task FinalizeLocalVotes(FinalizeLocalVote request, CancellationToken cancellationToken)
    {
        var approvers = await _cacheService.GetOrSetCache<IEnumerable<Approver>>("Approver", "All", async () => await _approverRepository.GetAll(cancellationToken), new TimeSpan(24, 0, 0));

        var tasks = approvers.Select(app =>
        {
            var electionApiService = CreateElectionApiService(app.Address);
            return electionApiService.FinalizeLocalVote(request);
        });

        await Task.WhenAll(tasks);
    }

    public async Task NotifyLocalVoteVerificationsCompleted(NotifyLocalVoteVerificationCompleted request, CancellationToken cancellationToken)
    {
        var approver = await _approverRepository.GetApproverWhoIsLeader(cancellationToken);
        var electionApiService = CreateElectionApiService(approver.Address);

        await electionApiService.NotifyLocalVoteVerificationCompleted(request);
    }

    public async Task RecordAcceptedVote(RecordAcceptedVote request, CancellationToken cancellationToken)
    {
        var approvers = await _cacheService.GetOrSetCache<IEnumerable<Approver>>("Approver", "All", async () => await _approverRepository.GetAll(cancellationToken) ,new TimeSpan(24, 0, 0));

        var tasks = approvers.Select(app =>
        {
            var electionApiService = CreateElectionApiService(app.Address);
            return electionApiService.RecordAcceptedVote(request);
        });

        await Task.WhenAll(tasks);
    }

    public async Task RegisterVotes(RegisterVote request, CancellationToken cancellationToken)
    {
        var approvers = await _cacheService.GetOrSetCache<IEnumerable<Approver>>("Approver", "All", async () => await _approverRepository.GetAll(cancellationToken), new TimeSpan(24, 0, 0));

        var tasks = approvers.Select(app =>
        {
            var electionApiService = CreateElectionApiService(app.Address);
            return electionApiService.RegisterVote(request);
        });

        await Task.WhenAll(tasks);

    }

    private IElectionApiService CreateElectionApiService(string url)
    {
        var httpClient = _httpClientFactory.CreateClient();
        httpClient.BaseAddress = new Uri(url);

        return RestService.For<IElectionApiService>(httpClient);
    }

}
