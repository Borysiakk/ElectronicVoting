using ElectronicVoting.Common.Domain.Table;
using ElectronicVoting.Common.Infrastructure;
using ElectronicVoting.Persistence;
using Microsoft.EntityFrameworkCore;


namespace ElectronicVoting.Infrastructure.Repository
{
    public interface ISettingRepository
    {
        public Task<Setting?> GetAsync(string parent, string child, CancellationToken cancellationToken);
    }
    public class SettingRepository : ISettingRepository
    {
        private readonly ICacheService _cacheService;
        private readonly CommonDbContext _dbContext;
        public SettingRepository(CommonDbContext dbContext, ICacheService cacheService)
        {
            _dbContext = dbContext;
            _cacheService = cacheService;
        }

        public async Task<Setting?> GetAsync(string parent, string child, CancellationToken cancellationToken)
        {
            var keyCache = "SettingRepository.GetAsync";

            var keyCacheParameters = parent + "." + child;

            var setting = _cacheService.GetFromCache<Setting>(keyCache, keyCacheParameters);

            if (setting == null)
            {
                setting = await _dbContext.Settings.FirstOrDefaultAsync(a => a.Name == parent && a.SubName == child, cancellationToken);
                _cacheService.AddToCache(keyCache, keyCacheParameters, setting,TimeSpan.FromDays(1));
            }

            return await _dbContext.Settings.FirstOrDefaultAsync(a => a.Name == parent && a.SubName == child, cancellationToken);
        }
    }
}
