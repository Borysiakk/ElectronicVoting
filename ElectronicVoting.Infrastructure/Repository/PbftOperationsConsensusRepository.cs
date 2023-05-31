
using System.Data;
using ElectronicVoting.Domain.Enum;
using ElectronicVoting.Domain.Table;
using ElectronicVoting.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace ElectronicVoting.Infrastructure.Repository
{
    public class PbftOperationsConsensusRepository : Repository<PbftOperationConsensus>
    {
        public PbftOperationsConsensusRepository(ApplicationDbContext dbContext) : base(dbContext){}

        public async Task<List<PbftOperationConsensus>> GetByStatus(PbftOperationStatus status = PbftOperationStatus.NotReady)
        {
            return await _dbSet.Where(a => a.Status == status).ToListAsync();
        }
    }
}
