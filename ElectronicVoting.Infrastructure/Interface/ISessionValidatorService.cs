using System.Threading.Tasks;
using ElectronicVoting.Domain.Entities;

namespace ElectronicVoting.Infrastructure.Interface
{
    public interface ISessionValidatorService
    {
        public Task AddAsync(SessionValidator session);
        public Task CloseAsync(string connectionId);
        public string GetConnectionIdByOrganization(string organization);
        public string GetOrganizationByConnectionId(string connectionId);
    }
}