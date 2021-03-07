using System.Threading.Tasks;
using ElectronicVoting.Domain.Contract.Requests;
using ElectronicVoting.Domain.Contract.Result;

namespace ElectronicVoting.Infrastructure.Interface
{
    public interface IAccountService
    {
        public Task<HttpAuthorizationResult> LoginAsync(LoginViewModel loginViewModel);
        public Task<HttpAuthorizationResult> RegisterAsync(RegisterViewModel registerViewModel);
    }
}