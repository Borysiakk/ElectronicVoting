using ElectronicVoting.Domain.Table;
using ElectronicVoting.Domain.Table.Main;
using ElectronicVoting.Persistence;
using Microsoft.EntityFrameworkCore;


namespace ElectronicVoting.Infrastructure.Repository
{
    public interface IValidatorRepository
    {
        Task<IEnumerable<Validator>> GetAllWithoutMe(CancellationToken cancellationToken);
    }

    public class ValidatorRepository : Repository<Validator>, IValidatorRepository
    {
        public ValidatorRepository(MainDbContext dbContext) : base(dbContext)
        {

        }

        public async Task<IEnumerable<Validator>> GetAllWithoutMe(CancellationToken cancellationToken)
        {
            var name = Environment.GetEnvironmentVariable("CONTAINER_NAME");
            return await this._dbSet.Where(a => a.Name != name).ToListAsync(cancellationToken);
        }
    }
}
