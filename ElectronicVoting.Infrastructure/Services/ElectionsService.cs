using System.Collections.Generic;
using ElectronicVoting.Infrastructure.Interface;

namespace ElectronicVoting.Infrastructure.Services
{
    public class ElectionsService :IElectionsService
    {
        private readonly ElectronicVotingManagement _votingManagement;

        public ElectionsService(ElectronicVotingManagement votingManagement)
        {
            _votingManagement = votingManagement;
        }

        public List<Candidate> GetCandidates()
        {
            return _votingManagement.CandidateManagement.Candidates;
        }
    }
}