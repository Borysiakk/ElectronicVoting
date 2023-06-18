
using ElectronicVoting.Common.Infrastructure;
using ElectronicVoting.Persistence;
using Microsoft.EntityFrameworkCore;
using Validator.Domain.Table.ChangeView;

namespace Validator.Infrastructure.Repository;
public interface IInitializationChangeViewTransactionRepository
{

}
public class InitializationChangeViewTransactionRepository : Repository<InitializationChangeViewTransaction>, IInitializationChangeViewTransactionRepository
{
    public InitializationChangeViewTransactionRepository(ValidatorDbContext dbContext) : base(dbContext) {}

}
