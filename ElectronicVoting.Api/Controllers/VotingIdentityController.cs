using System;
using System.Net;
using System.Threading.Tasks;
using ElectronicVoting.Domain.Contract.Requests;
using ElectronicVoting.Infrastructure.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicVoting.Api.Controllers
{
    [ApiController]
    [Route("api/VotingIdentity")]
    public class VotingIdentityController :ControllerBase
    {

        private readonly IVotingAccountService _votingAccountService;

        public VotingIdentityController(IVotingAccountService votingAccountService)
        {
            _votingAccountService = votingAccountService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            try
            {
                var result = await _votingAccountService.LoginAsync(login);
                if (result.Code == HttpStatusCode.Unauthorized)
                {
                    return Unauthorized(result);
                }

                return Ok(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        [HttpPost("Register")]
        public async Task<IActionResult> Register(VotingRegisterViewModel registerViewModel)
        {
            try
            {
                var result = await _votingAccountService.RegisterAsync(registerViewModel);
                if (result.Code == HttpStatusCode.Conflict)
                {
                    return Conflict(result);
                }

                return Ok(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}