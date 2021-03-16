using System;
using System.Threading.Tasks;
using ElectronicVoting.Domain.Contract.Requests;
using ElectronicVoting.Domain.Contract.Result;

namespace ElectronicVoting
{
    class Program
    {
        static async Task Main(string[] args)
        {
            LoginViewModel loginViewModel = new LoginViewModel()
            {
                Login = args[0],
                Password = "string",
            };

            var authorizationLogin = new AuthorizationLogin(loginViewModel);
            HttpOrganizationAuthorizationResult authorizationResult = await authorizationLogin.LoginAsync();
            
            Connection connection = new Connection();
            await connection.InitializationAsync(authorizationResult);

            var managementConnections = connection.ManagementConnectionsValidation;

            managementConnections.PriorityQueue.ActionAutoReadNode += () =>
            {
                
            };
            

            Console.ReadKey();
            await connection.Close();
        }
    }
}