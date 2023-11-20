using Validator.Infrastructure.EntityFramework;
namespace Validator.Infrastructure.Repository;

public interface IBaseRepository<T> where T : class
{
    Task<T> Add(T entity, CancellationToken cancellationToken);
}

public class GenericRepository<T> : IBaseRepository<T> where T : class
{
    protected ElectionDatabaseContext ElectionContext { get; set; }
    public GenericRepository(ElectionDatabaseContext electionContext)
    {
        ElectionContext = electionContext;
    }


    /// <inheritdoc />
    public async Task<T> Add(T entity, CancellationToken cancellationToken)
    {
        var entityResult = await ElectionContext.AddAsync<T>(entity, cancellationToken);
        await ElectionContext.SaveChangesAsync(cancellationToken);

        return entityResult.Entity;
    }
}
