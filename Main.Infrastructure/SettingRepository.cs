using ElectronicVoting.Domain.Table.Main;
using ElectronicVoting.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicVoting.Infrastructure.Repository
{
    public interface ISettingRepository
    {
        public Task<Setting?> GetAsync(string parent, string child, CancellationToken cancellationToken);
    }
    public class SettingRepository : ISettingRepository
    {
        private readonly MainDbContext _dbContext;
        public SettingRepository(MainDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Setting?> GetAsync(string parent, string child, CancellationToken cancellationToken)
        {
            return await _dbContext.Settings.FirstOrDefaultAsync(a => a.Name == parent && a.SubName == child, cancellationToken);
        }
    }
}
