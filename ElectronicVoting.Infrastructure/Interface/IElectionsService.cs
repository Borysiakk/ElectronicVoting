using System.Collections.Generic;

namespace ElectronicVoting.Infrastructure.Interface
{
    public interface IElectionsService
    {
        public List<Candidate> GetCandidates();
    }
}