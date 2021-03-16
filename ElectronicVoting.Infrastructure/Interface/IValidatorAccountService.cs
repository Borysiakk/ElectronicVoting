using System.Threading.Tasks;
using ElectronicVoting.Domain.Contract.Requests;
using ElectronicVoting.Domain.Contract.Result;

namespace ElectronicVoting.Infrastructure.Interface
{
    public interface IValidatorAccountService
    {
        public Task<HttpOrganizationAuthorizationResult> LoginAsync(LoginViewModel loginViewModel);
        public Task<HttpOrganizationAuthorizationResult> RegisterAsync(OrganizationRegisterViewModel registerViewModel);
    }
}