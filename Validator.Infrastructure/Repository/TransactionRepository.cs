using ElectronicVoting.Common.Infrastructure;
using ElectronicVoting.Persistence;
using ElectronicVoting.Validator.Domain.Table;


namespace ElectronicVoting.Infrastructure.Repository;
public class TransactionRepository : Repository<TransactionRegister>
{
    public TransactionRepository(ValidatorDbContext dbContext) : base(dbContext) {}
}
