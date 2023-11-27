using Main.Domain.Table;
using Main.Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Main.Infrastructure.Repository
{
    public interface ICandidateRepository
    {
        Task Add(Candidate candidate, CancellationToken cancellationToken);
        Task<Candidate> Get(long candidateId, CancellationToken cancellationToken);
        Task<IEnumerable<Candidate>> GetAll(CancellationToken cancellationToken);
    }

    public class CandidateRepository : ICandidateRepository
    {
        private readonly MainDbContext _mainDbContext;

        public CandidateRepository(MainDbContext mainDbContext)
        {
            _mainDbContext = mainDbContext;
        }

        public async Task Add(Candidate candidate, CancellationToken cancellationToken)
        {
            await _mainDbContext.AddAsync<Candidate>(candidate, cancellationToken);
            await _mainDbContext.SaveChangesAsync(cancellationToken);
        }

        public Task<Candidate> Get(long candidateId, CancellationToken cancellationToken)
        {
           return _mainDbContext.Candidates.FirstOrDefaultAsync(a => a.CandidateId == candidateId, cancellationToken);
        }

        public async Task<IEnumerable<Candidate>> GetAll(CancellationToken cancellationToken)
        {
            return await _mainDbContext.Candidates.ToListAsync<Candidate>(cancellationToken);
        }
    }
}
