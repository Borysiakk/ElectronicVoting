using System.Numerics;
using System.Text;

namespace ElectronicVoting.Math;

public static class BigIntegerUtils
{
    public static bool IsPrime(this BigInteger n,int k)
    {
        if(n.TestFermat(k) == true)
            return n.TestRabinMiller(k) == true;
        else
            return false;
    }
    
    private static bool TestFermat(this BigInteger n, int k)
    {
        for (var i = 0; i < k; i++) {
            var a = BigIntegerRandom.NextBigInteger(1, n-2);
            var r = BigInteger.ModPow(a, n - 1, n);
            if (r != 1) return false;
        }
        
        return true;
    }
    private static bool TestRabinMiller(this BigInteger n, int k)
    {
        var d = n - 1;
        while (d % 2 == 0) d /= 2;

        for (int i = 0; i < k; i++) {
            var a = BigIntegerRandom.NextBigInteger(2, n - 2);
            var x = BigInteger.ModPow(a, d, n);

            if (x == 1 || x == n - 1) 
                return true;

            while (d != n -1) {
                x = BigInteger.ModPow(x, 2, n);
                d *= 2;

                if (x == 1) return false;
                if (x == n - 1) return true;
            }
        }
        return false;
    }
    
    public static string ToBinaryString(this BigInteger bigint)
    {
        var bytes = bigint.ToByteArray();
        var idx = bytes.Length - 1;
        var base2 = new StringBuilder(bytes.Length * 8);
        var binary = Convert.ToString(bytes[idx], 2);
            
        if (binary[0] != '0' && bigint.Sign == 1)
            base2.Append('0');

        base2.Append(binary);
        for (idx--; idx >= 0; idx--)
            base2.Append(Convert.ToString(bytes[idx], 2).PadLeft(8, '0'));

        return base2.ToString().Substring(1,base2.ToString().Length-1);
    }
}