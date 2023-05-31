using ElectronicVoting.Domain.Table;
using ElectronicVoting.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicVoting.Infrastructure.Consensus
{
    public interface IPbftConsensus
    {
        public Task CommitAsync(PbftOperationConsensus operation);
        public Task PrepareAsync(PbftOperationConsensus operation);
        public Task PrePrepareAsync(PbftOperationConsensus operation);
    }

    public class PbftConsensus : IPbftConsensus
    {
        private readonly ValidatorRepository _validatorRepository;
        private readonly TransactionPendingRepository _transactionPendingRepository;

        public Task CommitAsync(PbftOperationConsensus operation)
        {
            throw new NotImplementedException();
        }

        public Task PrepareAsync(PbftOperationConsensus operation)
        {
            throw new NotImplementedException();
        }

        public Task PrePrepareAsync(PbftOperationConsensus operation)
        {
            throw new NotImplementedException();
        }
    }
}
