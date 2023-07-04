using ElectronicVoting.Persistence;
using Microsoft.EntityFrameworkCore;
using ElectronicVoting.Common.Infrastructure;
using Validator.Domain.Table;

namespace Validator.Infrastructure.Repository;

public interface IApproverRepository
{
    Task<Approver?> GetFirst(CancellationToken cancellationToken);
    Task<Approver?> GetByName(string name, CancellationToken cancellationToken);
    Task<IEnumerable<Approver>> GetAllWithoutMe(CancellationToken cancellationToken);
}

public class ApproverRepository : Repository<Approver>, IApproverRepository
{
    private readonly ICacheService _cacheService;
    public ApproverRepository(ValidatorDbContext dbContext, ICacheService cacheService) : base(dbContext)
    {
        _cacheService = cacheService;
    }

    public async Task<IEnumerable<Approver>> GetAllWithoutMe(CancellationToken cancellationToken)
    {
        var approvers = _cacheService.GetFromCache<List<Approver>>("ApproverRepository", "GetAllWithoutMe");

        if (approvers == null)
        {
            var name = Environment.GetEnvironmentVariable("CONTAINER_NAME");
            approvers = await this._dbSet.Where(a => a.Name != name).ToListAsync(cancellationToken);
            _cacheService.AddToCache("ApproverRepository", "GetAllWithoutMe", approvers, TimeSpan.FromDays(1));

            return approvers;
        }

        return approvers;
    }

    public async Task<Approver?> GetByName(string name, CancellationToken cancellationToken)
    {
        var keyCache = "ApproverRepository.GetByName";
        var approve = _cacheService.GetFromCache<Approver>(keyCache, name);

        if(approve == null)
        {
            approve = await _dbSet.FirstOrDefaultAsync(a => a.Name == name, cancellationToken);
            _cacheService.AddToCache(keyCache, name, approve, TimeSpan.FromDays(1));
        }

        return approve;
    }

    public async Task<Approver?> GetFirst(CancellationToken cancellationToken)
    {
        return await _dbSet.OrderBy(a=>a.Id).FirstOrDefaultAsync(cancellationToken);
    }
}
