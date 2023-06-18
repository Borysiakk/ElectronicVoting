using System.Security.Cryptography;
using Validator.Domain.Table;

namespace ElectronicVoting.Validator.Infrastructure.Helper;
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

