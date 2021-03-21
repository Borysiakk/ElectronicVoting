using System;
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
                r = 16191327;
            } while (BigInteger.GreatestCommonDivisor(r,n) != 1);
            
            Console.WriteLine(r);
            BigInteger C = BigInteger.Pow(n, 2);
            Console.WriteLine("1 {0}",C);
            Console.WriteLine("2 {0}",BigInteger.ModPow(g, msg, C));
            Console.WriteLine("3 {0}",BigInteger.ModPow(r, n, C));
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