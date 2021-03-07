
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ElectronicVoting.Domain.Contract.Requests;
using ElectronicVoting.Domain.Contract.Result;
using ElectronicVoting.Domain.Entities;
using ElectronicVoting.Infrastructure.Interface;
using Microsoft.AspNetCore.Identity;

namespace ElectronicVoting.Infrastructure.Services
{
    public class IdentityService :IIdentityService
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public IdentityService(UserManager<ApplicationUser> userManager, ITokenService tokenService, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
        }

        public async Task<HttpAuthorizationResult> LoginAsync(LoginViewModel loginModelView)
        {
            var result = await _signInManager.PasswordSignInAsync(loginModelView.Email, loginModelView.Password, loginModelView.RememberMe,false);
            if (!result.Succeeded)
            {
                return new HttpAuthorizationResult()
                {
                    Code = HttpStatusCode.Unauthorized,
                    Errors = new[] {"Login lub hasło są nie poprawne"},
                };
            }

            var user = await _userManager.FindByEmailAsync(loginModelView.Email);
            return new HttpAuthorizationResult()
            {
                OrganizationId = user.Id,
                Organization = user.UserName,
                DateIssue = DateTime.Now,
                Code = HttpStatusCode.OK,
                Token = _tokenService.Generate(user),
            };
        }

        public async Task<HttpAuthorizationResult> RegisterAsync(RegisterViewModel registerViewModel)
        {
            var user = new ApplicationUser() 
            {
                Id = Guid.NewGuid().ToString(),
                Email = registerViewModel.Email,
                UserName = registerViewModel.Email
            };
            
            var isUserExist = await _userManager.FindByEmailAsync(registerViewModel.Email);
            if (isUserExist != null)
            {
                return new HttpAuthorizationResult()
                {
                    Code = HttpStatusCode.Conflict,
                    Errors = new[] {"Znaleziono użytkownika o podanym mailu"},
                };
            }

            var result = await _userManager.CreateAsync(user, registerViewModel.Password);
            if (!result.Succeeded)
            {
                return new HttpAuthorizationResult()
                {
                    Code = HttpStatusCode.BadRequest,
                    Errors = result.Errors.Select(a => a.Description).ToList(),
                };
            }
            return new HttpAuthorizationResult()
            {
                OrganizationId = user.Id,
                Organization = user.UserName,
                Code = HttpStatusCode.OK,
                DateIssue = DateTime.Now,
                Token = _tokenService.Generate(user),
            };

        }
    }
}