using ElectronicVoting.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Validator.Domain.Table;
using Validator.Infrastructure.Service;

namespace Validator.Infrastructure.Repository
{
    public interface ISettingRepository : IBaseRepository<Setting>
    {
        public Task<Setting?> Get(string category, string name, CancellationToken cancellationToken);
    }

    public class SettingRepository : GenericRepository<Setting>, ISettingRepository
    {
        private readonly ICacheService _cacheService;

        public SettingRepository(ValidatorDbContext validatorDbContext, ICacheService cacheService) : base(validatorDbContext)
        {
            _cacheService = cacheService;
        }

        public async Task<Setting?> Get(string category, string name, CancellationToken cancellationToken)
        {
            var keyCache = "SettingRepository.GetAsync";
            var keyCacheParameters = category + "." + name;
            var setting = _cacheService.GetFromCache<Setting>(keyCache, keyCacheParameters);
            if (setting == null)
            {
                setting = await _validatorDbContext.Settings.FirstOrDefaultAsync(a => a.Category == category && a.Name == name, cancellationToken);
                _cacheService.AddToCache(keyCache, keyCacheParameters, setting, TimeSpan.FromHours(1));
            }

            return await _validatorDbContext.Settings.FirstOrDefaultAsync(a=>a.Category == category && a.Name == name, cancellationToken);
        }
    }
}
