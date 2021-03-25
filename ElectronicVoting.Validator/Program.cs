using System;
using System.Text;
using System.Threading.Tasks;
using ElectronicVoting.Domain.Contract.Requests;
using ElectronicVoting.Domain.Contract.Result;
using ElectronicVoting.Validator.PriorityQueue;

namespace ElectronicVoting.Validator
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                LoginViewModel loginViewModel = new LoginViewModel()
                {
                    Login = args.Length == 0 ? "borys59@onet.eu" :args[0],
                    Password = "string",
                };

                var authorizationLogin = new AuthorizationLogin(loginViewModel);
                HttpOrganizationAuthorizationResult authorizationResult = await authorizationLogin.LoginAsync();

                ConnectionManagement connectionManagement = ConnectionManagement.Factory.Create(authorizationResult);

                await connectionManagement.StartAsync();
                Console.WriteLine("Działanie Validatora");
                Console.ReadKey();
    
                await connectionManagement.StopAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}