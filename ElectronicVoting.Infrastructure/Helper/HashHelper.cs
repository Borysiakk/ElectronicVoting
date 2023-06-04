using ElectronicVoting.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicVoting.Infrastructure.Helper
{
    public class HashHelper
    {
        public static byte[] ComputeHash(Serialization obj)
        {
            using var alg = SHA512.Create();
            try
            {
                var objectArrayBytes = obj.Serialize();
                return alg.ComputeHash(objectArrayBytes);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
    }
}
