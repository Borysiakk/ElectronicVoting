using System.Threading.Tasks;
using ElectronicVoting.Domain.Contract.Requests;
using ElectronicVoting.Domain.Contract.Result;

namespace ElectronicVoting.Infrastructure.Interface
{
    public interface IIdentityService
    {
        public Task<HttpAuthorizationResult> LoginAsync(LoginViewModel loginModelView);
        public Task<HttpAuthorizationResult> RegisterAsync(RegisterViewModel registerViewModel);
    }
}