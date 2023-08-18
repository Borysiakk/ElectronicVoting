using ElectronicVoting.Persistence;
using Main.Domain.Table;
using Microsoft.EntityFrameworkCore;

namespace Main.Infrastructure.Repository;

public interface ICurrentLeaderRepository
{
    Task Add(CurrentLeader leader, CancellationToken cancellationToken);
    Task<CurrentLeader> GetLastLeader(CancellationToken cancellationToken);
}

public class CurrentLeaderRepository : ICurrentLeaderRepository
{
    private readonly MainDbContext _mainDbContext;

    public CurrentLeaderRepository(MainDbContext mainDbContext)
    {
        _mainDbContext = mainDbContext;
    }

    public async Task Add(CurrentLeader leader, CancellationToken cancellationToken)
    {
        await _mainDbContext.CurrentLeaders.AddAsync(leader, cancellationToken);
    }

    public async Task<CurrentLeader> GetLastLeader(CancellationToken cancellationToken)
    {
        return await _mainDbContext.CurrentLeaders.OrderBy(x => x.Id).LastOrDefaultAsync(cancellationToken);
    }
}
