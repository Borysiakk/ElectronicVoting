using System.Net;
using System.Threading.Tasks;
using ElectronicVoting.Domain.Contract.Requests;
using ElectronicVoting.Domain.Contract.Result;
using ElectronicVoting.Domain.Entities;
using ElectronicVoting.Infrastructure.Interface;
using ElectronicVoting.Persistence;
using Microsoft.EntityFrameworkCore;
using PaillierCryptoSystem.Model;
using BC = BCrypt.Net.BCrypt;

namespace ElectronicVoting.Infrastructure.Services
{
    public class VotingAccountService :IVotingAccountService
    {

        private readonly KeyPublic _keyPublic;
        private readonly ITokenService _tokenService;
        private readonly ApplicationDbContext _dbContext;

        public VotingAccountService(ApplicationDbContext dbContext, ITokenService tokenService, KeyPublic keyPublic)
        {
            _keyPublic = keyPublic;
            _dbContext = dbContext;
            _tokenService = tokenService;
        }

        private bool Authorization(string password, string hash)
        {
            return BC.Verify(password, hash);
        }
        
        public async Task<HttpVotingUserAuthorizationResult> LoginAsync(LoginViewModel loginViewModel)
        {
            var user = await _dbContext.VotingUsers.FirstOrDefaultAsync();
            if (user == null || !Authorization(loginViewModel.Password, user.PasswordHash))
            {
                return new HttpVotingUserAuthorizationResult()
                {
                    Code = HttpStatusCode.Unauthorized,
                    Errors = new[] {"Błędny login lub hasło"},
                };
            }
            
            return new HttpVotingUserAuthorizationResult()
            {
                Success = true,
                Name = user.Name,
                SurName = user.SurName,
                Code = HttpStatusCode.OK,
                KeyPublic = new KeyPublicModel()
                {
                    g = _keyPublic.g.ToString(),
                    n = _keyPublic.n.ToString(),
                    r = _keyPublic.r.ToString(),
                },
                Token = _tokenService.Generate(user),
            };
        }

        public async Task<HttpVotingUserAuthorizationResult> RegisterAsync(VotingRegisterViewModel registerViewModel)
        {
            var organization = await _dbContext.VotingUsers.FirstOrDefaultAsync(a => a.Email == registerViewModel.Email);
            
            if (registerViewModel.Password != registerViewModel.ConfirmPassword)
            {
                return new HttpVotingUserAuthorizationResult()
                {
                    Code = HttpStatusCode.Conflict,
                    Errors = new[] {"Hasła nie są identyczne"}
                };
            }
            
            if (organization != null)
            {
                return new HttpVotingUserAuthorizationResult()
                {
                    Success = false,
                    Code = HttpStatusCode.Conflict,
                    Errors = new[] {"Istnieje już konto powiązane z podanym adresem email"}
                };
            }

            VotingUser user = new VotingUser()
            {
                Name = registerViewModel.Name,
                SurName = registerViewModel.SurName,
                Email = registerViewModel.Email,
                PasswordHash = BC.HashPassword(registerViewModel.Password),
            };

            await _dbContext.VotingUsers.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            return new HttpVotingUserAuthorizationResult()
            {
                Name = registerViewModel.Name,
                SurName = registerViewModel.SurName,
                Code = HttpStatusCode.Created,
                Token = _tokenService.Generate(user),
            };
        }
    }
}