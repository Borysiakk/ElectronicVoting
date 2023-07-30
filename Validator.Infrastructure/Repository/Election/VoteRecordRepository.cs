using ElectronicVoting.Persistence;
using Validator.Domain.Table.Election;

namespace Validator.Infrastructure.Repository.Election;

public interface IVoteRecordRepository : IBaseRepository<VoteRecord>
{

}
internal class VoteRecordRepository : GenericRepository<VoteRecord>, IVoteRecordRepository
{
    public VoteRecordRepository(ValidatorDbContext validatorDbContext) : base(validatorDbContext) {}
}
