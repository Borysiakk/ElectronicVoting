using System.Numerics;
using PaillierCryptoSystem.Model;

namespace PaillierCryptosystem
{
    public interface IPaillier
    {
        public BigInteger Encryption(BigInteger msg, KeyPublic keyPublic);
        public BigInteger Decryption(BigInteger encrypted , KeyPrivate keyPrivate);
    }
}