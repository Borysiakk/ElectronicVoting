using ElectronicVoting.Math;
using System.Numerics;
namespace Math;

public class KeyPublic
{
    public BigInteger r { get; set; }
    public BigInteger g { get; set; }
    public BigInteger n { get; set; }
}
public class KeyPrivate
{
    public BigInteger p { get; set; }
    public BigInteger q { get; set; }
    public BigInteger mi { get; set; }
    public BigInteger lambda { get; set; }

}

public interface IKeyBuilder
{
    public void Generate(int bitLength, int k);
    public void Generate(BigInteger begin, BigInteger end, int k);
}

public class KeyBuilder : IKeyBuilder
{
    public KeyPublic KeyPublic { get; set; }
    public KeyPrivate KeyPrivate { get; set; }

    public void Generate(int bitLength, int k)
    {
        BigInteger g, r = 0;
        BigInteger[] key = new BigInteger[2];

        do
        {
            for (int i = 0; i < 2; i++)
            {
                do
                {
                    r = BigIntegerRandom.NextBigInteger(bitLength);

                } while (!r.IsPrime(k));

                key[i] = r;
            }

        } while (BigInteger.GreatestCommonDivisor(key[0] * key[1], (key[0] - 1) * (key[1])) == 1);

        BigInteger lambda = BigIntegerUtils.Lcm(key[0] - 1, key[1] - 1);
        BigInteger n = key[0] * key[1];

        do
        {

            g = BigIntegerRandom.NextBigInteger(1, BigInteger.Pow(n, 2));

        } while (BigInteger.GreatestCommonDivisor(g, n) != 1);

        BigInteger mi = BigIntegerUtils.ModularExponentiation(g, lambda, BigInteger.Pow(n, 2));
        BigInteger L = (mi - 1) / n;

        mi = BigIntegerUtils.ReciprocalModulo(L, n);

        KeyPublic = new KeyPublic()
        {
            n = n,
            g = g,
            r = r,
        };

        KeyPrivate = new KeyPrivate()
        {
            lambda = lambda,
            mi = mi,
            p = key[0],
            q = key[1],
        };
    }

    public void Generate(BigInteger begin, BigInteger end, int k)
    {
        BigInteger g, r = 0;
        BigInteger[] key = new BigInteger[2];

        for (int i = 0; i < 2; i++)
        {
            do
            {

                r = BigIntegerRandom.NextBigInteger(begin, end);

            } while (!r.IsPrime(k));

            key[i] = r;
        }

        BigInteger lambda = BigIntegerUtils.Lcm(key[0] - 1, key[1] - 1);
        BigInteger n = key[0] * key[1];

        do
        {

            g = BigIntegerRandom.NextBigInteger(1, BigInteger.Pow(n, 2));

        } while (BigInteger.GreatestCommonDivisor(g, n) != 1);

        BigInteger mi = BigIntegerUtils.ModularExponentiation(g, lambda, BigInteger.Pow(n, 2));
        BigInteger L = (mi - 1) / n;

        mi = BigIntegerUtils.ReciprocalModulo(L, n);

        KeyPublic = new KeyPublic()
        {
            n = n,
            g = g,
        };

        KeyPrivate = new KeyPrivate()
        {
            lambda = lambda,
            mi = mi,
            p = key[0],
            q = key[1],
        };
    }
}
