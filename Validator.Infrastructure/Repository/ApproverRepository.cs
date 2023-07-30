using ElectronicVoting.Persistence;
using Microsoft.EntityFrameworkCore;
using Validator.Domain.Table;
using Validator.Infrastructure.Service;

namespace Validator.Infrastructure.Repository;

public interface IApproverRepository : IBaseRepository<Approver>
{
    public Task<Int64> GetLastId(CancellationToken cancellationToken);
    public Task<Approver?> GetbyId(Int64 id, CancellationToken cancellationToke);
    public Task<Approver?> GetByName(string name, CancellationToken cancellationToken);
    public Task<IEnumerable<Approver>> GetAll(CancellationToken cancellationToken);
    public Task<IEnumerable<Approver>> GetAllWithout(string exceptApprover, CancellationToken cancellationToken);
}

public class ApproverRepository : GenericRepository<Approver>, IApproverRepository
{
    private readonly ICacheService _cacheService;
    public ApproverRepository(ValidatorDbContext validatorDbContext, ICacheService cacheService) : base(validatorDbContext)
    {
        _cacheService = cacheService;
    }

    public async Task<IEnumerable<Approver>> GetAll(CancellationToken cancellationToken)
    {
        var approvers = _cacheService.GetFromCache<List<Approver>>("ApproverRepository", "GetAll");
        if(approvers == null)
        {
            approvers = await _validatorDbContext.Approvers.ToListAsync(cancellationToken);
            _cacheService.AddToCache("ApproverRepository", "GetAll", approvers, TimeSpan.FromDays(1));
        }

        return approvers;
    }

    public async Task<IEnumerable<Approver>> GetAllWithout(string exceptApprover, CancellationToken cancellationToken)
    {
        var approvers = _cacheService.GetFromCache<List<Approver>>("ApproverRepository", "GetAllWithout");

        if (approvers == null)
        {
            var name = Environment.GetEnvironmentVariable("CONTAINER_NAME");
            approvers = await _validatorDbContext.Approvers.Where(a => a.Name != exceptApprover).ToListAsync(cancellationToken);
            _cacheService.AddToCache("ApproverRepository", "GetAllWithout", approvers, TimeSpan.FromDays(1));
        }

        return approvers;
    }

    public async Task<Approver?> GetbyId(Int64 id, CancellationToken cancellationToken)
    {
        return await _validatorDbContext.Approvers.FirstOrDefaultAsync(a => a.ApproverId == id, cancellationToken);
    }

    public async Task<Approver?> GetByName(string name, CancellationToken cancellationToken)
    {
        var keyCache = "ApproverRepository.GetByName";
        var approve = _cacheService.GetFromCache<Approver>(keyCache, name);

        if (approve == null)
        {
            approve = await _validatorDbContext.Approvers.FirstOrDefaultAsync(a=>a.Name == name, cancellationToken);
            _cacheService.AddToCache(keyCache, name, approve, TimeSpan.FromDays(1));
        }

        return approve;
    }

    public Task<Int64> GetLastId(CancellationToken cancellationToken)
    {
        return _validatorDbContext.Approvers.MaxAsync(a=>a.ApproverId, cancellationToken);
    }
}
