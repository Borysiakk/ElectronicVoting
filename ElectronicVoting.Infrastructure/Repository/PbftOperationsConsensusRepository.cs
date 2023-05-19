using ElectronicVoting.Domain.Table;
using ElectronicVoting.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicVoting.Infrastructure.Repository
{
    public class PbftOperationsConsensusRepository : Repository<PbftOperationConsensus>
    {
        public PbftOperationsConsensusRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
