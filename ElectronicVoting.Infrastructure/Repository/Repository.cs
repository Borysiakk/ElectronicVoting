using ElectronicVoting.Domain.Table;
using Microsoft.EntityFrameworkCore;

namespace ElectronicVoting.Infrastructure.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {

        public T? GetById(long id);
        public Task<T?> GetByIdAsync(long id, CancellationToken ct);

        public List<T> GetAll();
        public Task<List<T>> GetAllAsync(CancellationToken ct);

        public Task AddAsync(T entity, CancellationToken ct);
        public Task AddRangeAsync(IEnumerable<T> entities, CancellationToken ct);

        public void Update(T entity);
        public void UpdateRange(IEnumerable<T> entities);

        public Task SaveAsync(CancellationToken ct);
    }

    public abstract class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected DbSet<T> _dbSet;
        protected DbContext _dbContext;

        protected Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public async Task AddAsync(T entity, CancellationToken ct)
        {
            await _dbSet.AddAsync(entity, ct);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken ct)
        {
            await _dbSet.AddRangeAsync(entities, ct);
        }

        public List<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public async Task<List<T>> GetAllAsync(CancellationToken ct)
        {
            return await _dbSet.ToListAsync(ct);
        }

        public T? GetById(long id)
        {
            return _dbSet.FirstOrDefault(a => a.Id == id); 
        }

        public async Task<T?> GetByIdAsync(long id, CancellationToken ct)
        {
            return await _dbSet.FirstOrDefaultAsync(a => a.Id == id, ct);
        }

        public async Task SaveAsync(CancellationToken ct)
        {
            await _dbContext.SaveChangesAsync(ct);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void UpdateRange(IEnumerable<T> entities)
        {
            _dbSet.UpdateRange(entities);
        }
    }
}
