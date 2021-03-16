using System;
using System.Collections.Generic;

namespace ElectronicVoting
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> candidates = new List<string>()
            {
                "Borys","Mateusz","Krzysztof",
            };
            CandidateManagement candidateManagement = new CandidateManagement(candidates,15);

            Console.WriteLine(candidateManagement.Candidates[2].Voice);
            Console.WriteLine(candidateManagement.Candidates[2].Name);
        }
    }
}