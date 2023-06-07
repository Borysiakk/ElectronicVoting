using ElectronicVoting.Domain.Table.Blockchain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicVoting.Infrastructure.Services
{
    public interface ITransactionService
    {
        public Transaction Create(Int64 voice);
    }

    public class TransactionService : ITransactionService
    {
        public Transaction Create(long voice)
        {
            var transaction = new Transaction()
            {
                Voice = voice
            };

            return transaction;
        }
    }
}
