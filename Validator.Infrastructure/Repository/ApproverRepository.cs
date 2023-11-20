using Microsoft.EntityFrameworkCore;
using Validator.Domain.Table.Electronic;
using Validator.Infrastructure.EntityFramework;

namespace Validator.Infrastructure.Repository
{
    public interface IApproverRepository
    {
        Task<long> GetLastId(CancellationToken cancellationToken);
        Task<Approver> GetById(long id, CancellationToken cancellationToken); 
        Task<Approver> GetByName(string name, CancellationToken cancellationToken);
        Task<IEnumerable<Approver>> GetAll(CancellationToken cancellationToken);
        Task<IEnumerable<Approver>> GetAllWithoutMe(string exceptApprover, CancellationToken cancellationToken);

        Task<Approver> GetApproverWhoIsLeader(CancellationToken cancellationToken);
    }

    public sealed class ApproverRepository : GenericRepository<Approver>, IApproverRepository
    {
        public ApproverRepository(ElectionDatabaseContext electionContext) : base(electionContext)
        {

        }

        public Task<Approver> GetById(long id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<Approver> GetByName(string name, CancellationToken cancellationToken)
        {
            return await ElectionContext.Approvers.FirstOrDefaultAsync(a => a.Name == name, cancellationToken);
        }

        public async Task<IEnumerable<Approver>> GetAll(CancellationToken cancellationToken)
        {
            return await ElectionContext.Approvers.ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Approver>> GetAllWithoutMe(string exceptApprover, CancellationToken cancellationToken)
        {
            return await ElectionContext.Approvers.Where(a => a.Name != exceptApprover).ToListAsync(cancellationToken);
        }

        public async Task<long> GetLastId(CancellationToken cancellationToken)
        {
            return await ElectionContext.Approvers.MaxAsync(a => a.ApproverId, cancellationToken);
        }

        public async Task<Approver> GetApproverWhoIsLeader(CancellationToken cancellationToken)
        {
            var leaderId = await ElectionContext.Leaders.MaxAsync(a => a.LeaderId, cancellationToken);
            var approverId  = await ElectionContext.Leaders.Where(a=>a.LeaderId == leaderId).Select(a=>a.ApproverId).FirstOrDefaultAsync(cancellationToken);
            return await ElectionContext.Approvers.FirstOrDefaultAsync(a => a.ApproverId == approverId, cancellationToken);
        }
    }
}
