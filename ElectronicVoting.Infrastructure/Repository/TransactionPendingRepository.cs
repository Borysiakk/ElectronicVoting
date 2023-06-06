using ElectronicVoting.Domain.Table;
using ElectronicVoting.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicVoting.Infrastructure.Repository
{

    public interface ITransactionPendingRepository
    {
        public Int64 CountTransactionPendingByIdAndByHash(string transactionId, byte[] hash);
    }
    public class TransactionPendingRepository : Repository<TransactionPending>, ITransactionPendingRepository
    {
        public TransactionPendingRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public long CountTransactionPendingByIdAndByHash(string transactionId, byte[] hash)
        {
            return _dbSet.Count(a => a.TransactionId == transactionId && a.Hash.SequenceEqual(hash));
        }
    }
}
