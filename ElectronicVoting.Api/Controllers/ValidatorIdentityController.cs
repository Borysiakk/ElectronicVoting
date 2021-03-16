using System;
using System.Net;
using System.Threading.Tasks;
using ElectronicVoting.Domain.Contract.Requests;
using ElectronicVoting.Infrastructure.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicVoting.Api.Controllers
{
    [ApiController]
    [Route("api/ValidatorIdentity")]
    public class ValidatorIdentityController:ControllerBase
    {
        private readonly IValidatorAccountService _validatorAccountService;

        public ValidatorIdentityController(IValidatorAccountService validatorAccountService)
        {
            _validatorAccountService = validatorAccountService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            try
            {
                var result = await _validatorAccountService.LoginAsync(login);
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
        public async Task<IActionResult> Register(OrganizationRegisterViewModel registerViewModel)
        {
            try
            {
                var result = await _validatorAccountService.RegisterAsync(registerViewModel);
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