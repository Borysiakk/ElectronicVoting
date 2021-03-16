using System.Net;
using System.Threading.Tasks;
using ElectronicVoting.Domain.Contract.Requests;
using ElectronicVoting.Domain.Contract.Result;
using ElectronicVoting.Domain.Entities;
using ElectronicVoting.Infrastructure.Interface;
using ElectronicVoting.Persistence;
using Microsoft.EntityFrameworkCore;
using BC = BCrypt.Net.BCrypt;

namespace ElectronicVoting.Infrastructure.Services
{
    public class ValidatorAccountService :IValidatorAccountService
    {
        private readonly ITokenService _tokenService;
        private readonly ApplicationDbContext _dbContext;

        public ValidatorAccountService(ITokenService tokenService, ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _tokenService = tokenService;
        }

        private bool Authorization(string password, string hash)
        {
            return BC.Verify(password, hash);
        }
        
        public async Task<HttpOrganizationAuthorizationResult> LoginAsync(LoginViewModel loginViewModel)
        {
            var organization = await _dbContext.ValidatorUsers.FirstOrDefaultAsync();
            if (organization == null || !Authorization(loginViewModel.Password, organization.PasswordHash))
            {
                return new HttpOrganizationAuthorizationResult()
                {
                    Code = HttpStatusCode.Unauthorized,
                    Errors = new[] {"Błędny login lub hasło"},
                };
            }

            return new HttpOrganizationAuthorizationResult()
            {
                Success = true,
                Code = HttpStatusCode.OK,
                OrganizationId = organization.Id,
                Organization = organization.Organization,
                Token = _tokenService.Generate(organization),
            };
        }

        public async Task<HttpOrganizationAuthorizationResult> RegisterAsync(OrganizationRegisterViewModel registerViewModel)
        {
            var organization = await _dbContext.ValidatorUsers.FirstOrDefaultAsync(a => a.Email == registerViewModel.Email);
            
            if (registerViewModel.Password != registerViewModel.ConfirmPassword)
            {
                return new HttpOrganizationAuthorizationResult()
                {
                    Code = HttpStatusCode.Conflict,
                    Errors = new[] {"Hasła nie są identyczne"}
                };
            }
            
            if (organization != null)
            {
                return new HttpOrganizationAuthorizationResult()
                {
                    Success = false,
                    Code = HttpStatusCode.Conflict,
                    Errors = new[] {"Istnieje już konto powiązane z podanym adresem email"}
                };
            }

            ValidatorUser user = new ValidatorUser()
            {
                Email = registerViewModel.Email,
                Organization = registerViewModel.Organization,
                PasswordHash = BC.HashPassword(registerViewModel.Password),
            };

            await _dbContext.ValidatorUsers.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            return new HttpOrganizationAuthorizationResult()
            {
                Code = HttpStatusCode.Created,
                Token = _tokenService.Generate(user),
            };
        }
    }
}