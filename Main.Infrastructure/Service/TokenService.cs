using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Main.Infrastructure.Service
{
    public interface ITokenService
    {
        public Guid GenerateToken();
    }
    public class TokenService : ITokenService
    {
        public Guid GenerateToken()
        {
            using var provider = new RNGCryptoServiceProvider();
            var bytes = new byte[16];

            provider.GetBytes(bytes);

            return new Guid(bytes);
        }
    }
}
