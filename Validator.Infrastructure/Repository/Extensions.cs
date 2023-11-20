using Microsoft.Extensions.DependencyInjection;
using Validator.Infrastructure.Repository.Blockchain;
using Validator.Infrastructure.Repository.Election;

namespace Validator.Infrastructure.Repository
{
    public static class Extensions
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ISettingRepository, SettingRepository>();
            services.AddScoped<ILeaderRepository, LeaderRepository>();
            services.AddScoped<IApproverRepository, ApproverRepository>();
            services.AddScoped<IPendingLocalVoteRepository, PendingLocalVoteRepository>();
            services.AddScoped<IPendingLeaderVoteRepository, PendingLeaderVoteRepository>();
            services.AddScoped<IVoteConfirmedRepository, VoteConfirmedRepository>();
            services.AddScoped<IVoteRecordRepository, VoteRecordRepository>();
            services.AddScoped<IBlockRepository, BlockRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
        }
    }
}
