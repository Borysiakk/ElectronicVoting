using ElectronicVoting.Math;
using System.Numerics;

namespace Math;
public interface IPaillier
{
    public BigInteger Encryption(BigInteger msg, KeyPublic keyPublic);
    public BigInteger Decryption(BigInteger encrypted, KeyPrivate keyPrivate);
}

public class Paillier : IPaillier
{
    public BigInteger Decryption(BigInteger encrypted, KeyPrivate keyPrivate)
    {
        BigInteger mi = keyPrivate.mi;
        BigInteger lambda = keyPrivate.lambda;
        BigInteger n = keyPrivate.p * keyPrivate.q;

        BigInteger L = BigInteger.ModPow(encrypted, lambda, BigInteger.Pow(n, 2));
        L = BigInteger.Divide(L - 1, n);

        return (L * mi) % n;
    }

    public BigInteger Encryption(BigInteger msg, KeyPublic keyPublic)
    {
        BigInteger r = 0;
        BigInteger g = keyPublic.g;
        BigInteger n = keyPublic.n;

        do
        {
            r = BigIntegerRandom.NextBigInteger(1,n); ///check n or n^2 --> n  = Zn or n Zn*
        } while (BigInteger.GreatestCommonDivisor(r, n) != 1);

        BigInteger C = BigInteger.Pow(n, 2);
        return BigInteger.ModPow(g, msg, C) * BigInteger.ModPow(r, n, C);
    }
}
