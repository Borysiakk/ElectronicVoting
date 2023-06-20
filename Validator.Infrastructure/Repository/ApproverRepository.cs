using ElectronicVoting.Persistence;
using Microsoft.EntityFrameworkCore;
using ElectronicVoting.Common.Infrastructure;
using Validator.Domain.Table;

namespace ElectronicVoting.Infrastructure.Repository;

public interface IApproverRepository
{
    Task<Approver> GetByName(string name, CancellationToken cancellationToken);
    Task<IEnumerable<Approver>> GetAllWithoutMe(CancellationToken cancellationToken);
}

public class ApproverRepository : Repository<Approver>, IApproverRepository
{
    public ApproverRepository(ValidatorDbContext dbContext) : base(dbContext) {}

    public async Task<IEnumerable<Approver>> GetAllWithoutMe(CancellationToken cancellationToken)
    {
        var name = Environment.GetEnvironmentVariable("CONTAINER_NAME");
        return await this._dbSet.Where(a => a.Name != name).ToListAsync(cancellationToken);
    }

    public async Task<Approver> GetByName(string name, CancellationToken cancellationToken)
    {
        return await _dbSet.FirstOrDefaultAsync(a => a.Name == name, cancellationToken);
    }
}
