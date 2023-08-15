using ElectronicVoting.Persistence;
using Microsoft.EntityFrameworkCore.Storage;

namespace Validator.Infrastructure.Repository;

public interface ITransaction
{
    IDbContextTransaction OpenTransaction();

    void CommitTransaction(IDbContextTransaction transaction);

    void RollbackTransaction(IDbContextTransaction transaction);
}
public interface IBaseRepository<T> where T : class
{
    Task<T> Add (T entity, CancellationToken cancellationToken);
    Task AddRange (IEnumerable<T> entites, CancellationToken cancellationToken); 
}

public class GenericRepository<T> : IBaseRepository<T>, ITransaction where T : class
{
    protected ValidatorDbContext _validatorDbContext { get; set; }
    public GenericRepository(ValidatorDbContext validatorDbContext)
    {
        _validatorDbContext = validatorDbContext;
    }
    public void CommitTransaction(IDbContextTransaction transaction)
    {
        transaction.Commit();
    }

    public IDbContextTransaction OpenTransaction()
    {
        return _validatorDbContext.Database.BeginTransaction();
    }

    public void RollbackTransaction(IDbContextTransaction transaction)
    {
        transaction.Rollback();
    }

    public async Task<T> Add(T entity, CancellationToken cancellationToken)
    {
        var entityResult = await _validatorDbContext.AddAsync<T>(entity, cancellationToken);
        await _validatorDbContext.SaveChangesAsync(cancellationToken);

        return entityResult.Entity;
    }

    public async Task AddRange(IEnumerable<T> entites, CancellationToken cancellationToken)
    {
        await _validatorDbContext.AddRangeAsync(entites, cancellationToken);
        await _validatorDbContext.SaveChangesAsync(cancellationToken);
    }
}
