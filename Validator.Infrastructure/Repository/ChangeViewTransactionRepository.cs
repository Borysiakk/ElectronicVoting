using ElectronicVoting.Persistence;
using Validator.Domain.Table.ChangeView;
using ElectronicVoting.Common.Infrastructure;

namespace Validator.Infrastructure.Repository;

public interface IChangeViewTransactionRepository
{

}

public class ChangeViewTransactionRepository : Repository<ChangeViewTransaction>, IChangeViewTransactionRepository
{
    public ChangeViewTransactionRepository(ValidatorDbContext dbContext) : base(dbContext) {}
}
