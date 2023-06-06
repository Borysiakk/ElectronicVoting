using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

namespace ElectronicVoting.Infrastructure.Repository
{
    public interface ITransaction
    {
        public Task<IDbContextTransaction> CreateTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.ReadUncommitted, CancellationToken cancellationToken = default);
    }
}
