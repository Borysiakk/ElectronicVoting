using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Validator.Infrastructure.Repository;
using Validator.Infrastructure.Repository.ChangeLeader;
using Validator.Infrastructure.Repository.Election;
using Validator.Infrastructure.Service;
using Validator.Infrastructure.Service.ChangeLeader;
using Validator.Infrastructure.Service.Election;

namespace Validator.Infrastructure
{
    public static class Extension
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection service)
        {
            service.AddMemoryCache();
            service.AddScoped<ICacheService, CacheService>();
            service.AddScoped<IApproverRepository, ApproverRepository>();
            service.AddScoped<IApproverService, ApproverService>();
            service.AddScoped<IPreChangeLeaderService, PreChangeLeaderService>();
            service.AddScoped<IPreLocalVoteChangeLeaderService, PreLocalVoteChangeLeaderService>();
            service.AddScoped<IPreLeaderVoteChangeLeaderService, PreLeaderVoteChangeLeaderService>();
            service.AddScoped<ISettingRepository, SettingRepository>();
            service.AddScoped<IPreLocalVoteChangeLeaderRepository, PreLocalVoteChangeLeaderRepository>();
            service.AddScoped<IPreLeaderVoteChangeLeaderRepository, PreLeaderVoteChangeLeaderRepository>();
            service.AddScoped<IPreLocalVoteChangeLeaderHistoryRepository, PreLocalVoteChangeLeaderHistoryRepository>();
            service.AddScoped<IPreLeaderVoteChangeLeaderHistoryRepository, PreLeaderVoteChangeLeaderHistoryRepository>();
            service.AddScoped<ILeaderRepository, LeaderRepository>();
            service.AddScoped<ILeaderService, LeaderService>();
            service.AddScoped<ILocalVoteChangeLeaderRepository, LocalVoteChangeLeaderRepository>();
            service.AddScoped<ILeaderVoteChangeLeaderRepository, LeaderVoteChangeLeaderRepository>();
            service.AddScoped<ILocalVoteChangeLeaderService, LocalVoteChangeLeaderService>();
            service.AddScoped<ILeaderVoteChangeLeaderService, LeaderVoteChangeLeaderService>();
            service.AddScoped<ILocalVoteChangeLeaderHistoryRepository, LocalVoteChangeLeaderHistoryRepository>();
            service.AddScoped<ILeaderVoteChangeLeaderHistoryRepository, LeaderVoteChangeLeaderHistoryRepository>();
            service.AddScoped<IPendingLocalVoteRepository, PendingLocalVoteRepository>();
            service.AddScoped<IVoteRecordRepository, VoteRecordRepository>();
            service.AddScoped<IProofOfKnowledgeService, ProofOfKnowledgeService>();
            service.AddScoped<IVoteRecordService, VoteRecordService>();
            service.AddScoped<IPendingLocalVoteRepository, PendingLocalVoteRepository>();
            service.AddScoped<IPendingLocalVoteService, PendingLocalVoteService>();
            service.AddScoped<IPendingLocalVoteHistoryRepository, PendingLocalVoteHistoryRepository>();
            service.AddScoped<IPendingLeaderVoteRepository, PendingLeaderVoteRepository>();
            
            service.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            return service;
        }
    }
}
