using System.Threading.Tasks;
using ElectronicVoting.Domain.Contract.Requests;
using ElectronicVoting.Domain.Contract.Result;

namespace ElectronicVoting.Infrastructure.Interface
{
    public interface IVotingAccountService
    {
        public Task<HttpVotingUserAuthorizationResult> LoginAsync(LoginViewModel loginViewModel);
        public Task<HttpVotingUserAuthorizationResult> RegisterAsync(VotingRegisterViewModel registerViewModel);
    }
}