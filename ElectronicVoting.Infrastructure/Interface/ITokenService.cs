

using ElectronicVoting.Domain.Entities;

namespace ElectronicVoting.Infrastructure.Interface
{
    public interface ITokenService
    {
        public string Generate(ApplicationUser user);
    }
}