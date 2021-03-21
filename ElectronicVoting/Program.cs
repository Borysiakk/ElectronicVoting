using System;
using System.Collections.Generic;
using System.Numerics;
using PaillierCryptoSystem;
using PaillierCryptoSystem.Model;

namespace ElectronicVoting
{
    class Program
    {
        static void Main(string[] args)
        {
            Paillier paillier = new Paillier();
            
            List<string> candidates = new List<string>()
            {
                "Borys","Mateusz","Krzysztof",
            };
            CandidateManagement candidateManagement = new CandidateManagement(candidates,2000);

            // KeyPublic keyPublic = new KeyPublic()
            // {
            //     g = BigInteger.Parse("5339628513143902558795481621176332717257693704069863196315887856465578535078166732092202350175"),
            //     n = BigInteger.Parse("748721503486400539362029195476039017907077273581"),
            //     r = BigInteger.Parse("1198624387395498452763713")
            // };

            KeyPublic keyPublic = new KeyPublic()
            {
                g = BigInteger.Parse("30990084689774742"),
                n = BigInteger.Parse("382780379"),
                r = BigInteger.Parse("9203")
            };
            
            // KeyPrivate keyPrivate = new KeyPrivate()
            // {
            //     lambda = BigInteger.Parse("187180375871600134840506843050250240005945980608"),
            //     mi = BigInteger.Parse("24637984135631551203958812591332580109071421690"),
            //     p = BigInteger.Parse("624650650662384840587437"),
            //     q = BigInteger.Parse("1198624387395498452763713")
            // };
            //
            KeyPrivate keyPrivate = new KeyPrivate()
            {
                lambda = BigInteger.Parse("191364792"),
                mi = BigInteger.Parse("300160654"),
                p = BigInteger.Parse("41593"),
                q = BigInteger.Parse("9203")
            };


            var t = paillier.Encryption(20, keyPublic);
            var c = BigInteger.Parse("14305948187877847400313472765919232");
            var msg = paillier.Decryption(c, keyPrivate);
            Console.WriteLine(t);
        }
    }
}