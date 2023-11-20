using Microsoft.Extensions.DependencyInjection;
using Validator.Application.Service.Election;

namespace Validator.Application.Service;

public static class Extensions
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<ILeaderService, LeaderService>();
        services.AddScoped<IApproverService, ApproverService>();
        services.AddScoped<IProofOfKnowledgeService, ProofOfKnowledgeService>();
        services.AddScoped<IPendingVoteService, PendingVoteService>();
        services.AddScoped<IElectionService, ElectionService>();
    }
}
