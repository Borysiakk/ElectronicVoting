using System.Threading.Tasks;
using ElectronicVoting.Domain.Contract.Requests;
using ElectronicVoting.Domain.Contract.Result;
using ElectronicVoting.Http;

namespace ElectronicVoting
{
    public class AuthorizationLogin
    {
        private readonly LoginViewModel _loginViewModel;

        public AuthorizationLogin(LoginViewModel loginViewModel)
        {
            _loginViewModel = loginViewModel;
        }

        public async Task<HttpOrganizationAuthorizationResult> LoginAsync()
        {
            return await HttpHelper.Post<HttpOrganizationAuthorizationResult, LoginViewModel>(Routes.Identity.Login,_loginViewModel);
        }
    }
}