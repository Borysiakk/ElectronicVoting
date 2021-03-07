using System.Linq;
using System.Threading.Tasks;
using ElectronicVoting.Domain.Entities;
using ElectronicVoting.Infrastructure.Interface;
using ElectronicVoting.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ElectronicVoting.Infrastructure.Services
{
    public class SessionValidatorService :ISessionValidatorService
    {
        private readonly ApplicationDbContext _dbContext;

        public SessionValidatorService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(SessionValidator session)
        {
            await _dbContext.SessionValidators.AddAsync(session);
            await _dbContext.SaveChangesAsync();
        }

        public async Task CloseAsync(string connectionId)
        {
            var session = await _dbContext.SessionValidators.Where(a => a.StatusConnection == true)
                                          .FirstOrDefaultAsync(b => b.ConnectionId == connectionId);
            session.StatusConnection = false;
            _dbContext.SessionValidators.Update(session);
            await _dbContext.SaveChangesAsync();
        }

        public string GetConnectionIdByOrganization(string organization)
        {
            return _dbContext.SessionValidators.Where(a => a.StatusConnection == true).FirstOrDefault(b => b.Organization == organization)?.ConnectionId;
        }

        public string GetOrganizationByConnectionId(string connectionId)
        {
            return _dbContext.SessionValidators.Where(a => a.StatusConnection == true).FirstOrDefault(b => b.ConnectionId == connectionId)?.Organization;
        }
    }
}