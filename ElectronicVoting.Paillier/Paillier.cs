using System.Numerics;
using PaillierCryptosystem;
using PaillierCryptoSystem.Model;

namespace PaillierCryptoSystem
{
    public class Paillier :IPaillier 
    {
        public BigInteger Encryption(BigInteger msg, KeyPublic keyPublic)
        {
            BigInteger r = 0;
            BigInteger g = keyPublic.g;
            BigInteger n = keyPublic.n;
            
            do
            {
                r = BigIntegerRandom.NextBigInteger(1,n);
            } while (BigInteger.GreatestCommonDivisor(r,n) != 1);
            
            BigInteger C = BigInteger.Pow(n, 2);

            return BigInteger.ModPow(g, msg, C) * BigInteger.ModPow(r, n, C);

        }

        public BigInteger Decryption(BigInteger encrypted, KeyPrivate keyPrivate)
        {
            BigInteger mi = keyPrivate.mi;
            BigInteger lambda = keyPrivate.lambda;
            BigInteger n = keyPrivate.p * keyPrivate.q;

            BigInteger L = BigInteger.ModPow(encrypted, lambda, BigInteger.Pow(n, 2));

            //L = (L - 1) / n;
            L = BigInteger.Divide(L - 1, n);
            
            return (L * mi) % n;
        }
    }
}