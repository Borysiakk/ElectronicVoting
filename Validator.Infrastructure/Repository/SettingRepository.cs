using ElectronicVoting.Common.Domain.Table;
using ElectronicVoting.Persistence;
using ElectronicVoting.Validator.Domain.Table;
using Microsoft.EntityFrameworkCore;


namespace ElectronicVoting.Infrastructure.Repository
{
    public interface ISettingRepository
    {
        public Task<Setting?> GetAsync(string parent, string child, CancellationToken cancellationToken);
    }
    public class SettingRepository : ISettingRepository
    {
        private readonly CommonDbContext _dbContext;
        public SettingRepository(CommonDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Setting?> GetAsync(string parent, string child, CancellationToken cancellationToken)
        {
            return await _dbContext.Settings.FirstOrDefaultAsync(a => a.Name == parent && a.SubName == child, cancellationToken);
        }
    }
}
