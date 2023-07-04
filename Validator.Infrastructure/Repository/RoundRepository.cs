using ElectronicVoting.Common.Infrastructure;
using ElectronicVoting.Persistence;
using Microsoft.EntityFrameworkCore;
using Validator.Domain.Table.ChangeView;

namespace Validator.Infrastructure.Repository
{
    public interface IRoundRepository
    {
        Task<Round?> GetLastRound(CancellationToken ct);
        Task<Int64> GetNextApprover(CancellationToken ct);
    }
    public class RoundRepository : Repository<Round>, IRoundRepository
    {
        private readonly ApproverRepository _approverRepository;
        public RoundRepository(ValidatorDbContext dbContext, ApproverRepository approverRepository) : base(dbContext)
        {
            _approverRepository = approverRepository;
        }

        public async Task<Round?> GetLastRound(CancellationToken ct)
        {
            return await _dbSet.OrderBy(a=>a.Id).LastOrDefaultAsync(ct);
        }

        public async Task<Int64> GetNextApprover(CancellationToken ct)
        {
            var lastRound = await _dbSet.OrderBy(a => a.Id).LastOrDefaultAsync(ct);
            var approver = await _approverRepository.GetByIdAsync(lastRound.ApproverId + 1, ct);

            if( approver == null)
            {
                var firstApprover = await _approverRepository.GetFirst(ct);
                return firstApprover.Id;
            }

            return approver.Id;
        }
    }
}
