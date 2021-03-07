using System;
using System.Net;
using System.Threading.Tasks;
using ElectronicVoting.Domain.Contract.Requests;
using ElectronicVoting.Infrastructure.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicVoting.Web.Controllers.Api
{
    [ApiController]
    [Route("api/Identity")]
    public class IdentityController :ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IIdentityService _identityService;

        public IdentityController(IIdentityService identityService, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _identityService = identityService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            try
            {
                var result = await _identityService.LoginAsync(login);
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
        public async Task<IActionResult> Register(RegisterViewModel register)
        {
            try
            {
                var result = await _identityService.RegisterAsync(register);
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