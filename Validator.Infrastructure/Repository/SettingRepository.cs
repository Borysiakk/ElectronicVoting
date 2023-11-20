using Microsoft.EntityFrameworkCore;
using Validator.Domain.Table;
using Validator.Infrastructure.Cache;
using Validator.Infrastructure.EntityFramework;

namespace Validator.Infrastructure.Repository;

public interface ISettingRepository : IBaseRepository<Setting>
{
    public Task<Setting> Get(string category, string name, CancellationToken cancellationToken);
}

public class SettingRepository : GenericRepository<Setting>, ISettingRepository
{
    private readonly ICacheService _cacheService;

    public SettingRepository(ElectionDatabaseContext electionDatabaseContext, ICacheService cacheService) : base(electionDatabaseContext)
    {
        _cacheService = cacheService;
    }

    public async Task<Setting> Get(string category, string name, CancellationToken cancellationToken)
    {
        var keyCache = "SettingRepository.GetAsync";
        var keyCacheParameters = category + "." + name;
        var setting = _cacheService.GetFromCache<Setting>(keyCache, keyCacheParameters);
        if (setting == null)
        {
            setting = await ElectionContext.Settings.FirstOrDefaultAsync(a => a.Category == category && a.Name == name, cancellationToken);
            _cacheService.AddToCache(keyCache, keyCacheParameters, setting, TimeSpan.FromHours(1));
        }

        return await ElectionContext.Settings.FirstOrDefaultAsync(a => a.Category == category && a.Name == name, cancellationToken);
    }
}
