using System.Collections.Generic;
using PaillierCryptoSystem;

namespace ElectronicVoting
{
    public class CandidateManagement
    {
        public int Voters { get; private set; }
        public List<Candidate> Candidates { get; private set; }

        public CandidateManagement(List<string> candidates,int voters )
        {
            Candidates = new List<Candidate>(candidates.Count);
            int countVotersBits = BigIntegerUtils.ToBinaryString(voters).Length;
            
            foreach (var candidate in candidates)
            {
                Candidates.Add(new Candidate()
                {
                    Name = candidate,
                    Voice = (1 << (Candidates.Count * countVotersBits)),
                });
            }
        }
    }
}