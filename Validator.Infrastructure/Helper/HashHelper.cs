using ProtoBuf;
using System.Security.Cryptography;

namespace Validator.Infrastructure.Helper;

public  class HashHelper
{
    public static byte[] ComputeHash<T>( T obj)
    {
        using var alg = SHA512.Create();
        try
        {
            var objectArrayBytes = ObjectToByteArray<T>(obj);
            return alg.ComputeHash(objectArrayBytes);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    private static byte[] ObjectToByteArray<T>( T obj)
    {
        using (var stream = new MemoryStream())
        {
            Serializer.Serialize(stream, obj);
            return stream.ToArray();
        }
    }
}
