using System.Collections.Generic;
using PaillierCryptoSystem;
using PaillierCryptoSystem.Model;

namespace ElectronicVoting
{
    public class ElectronicVotingManagement
    {
        public KeyPublic KeyPublic { get; private set; } 
        public KeyPrivate KeyPrivate { get; private set; }
        public CandidateManagement CandidateManagement { get; set; }

        public ElectronicVotingManagement()
        {
            List<string> candidates = new List<string>()
            {
                "Borys Szyma","Mateusz Szczota","Krzysztof Krawczyk"
            };
            CandidateManagement = new CandidateManagement(candidates, 2000);
            
            KeyBuilder builder = new KeyBuilder();
            builder.Generate(16,100);

            KeyPublic = builder.KeyPublic;
            KeyPrivate = builder.KeyPrivate;
        }
    }
}