using System.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace ElectronicVoting.Cammon.Interface;
public interface ITransaction
{
    public Task<IDbContextTransaction> CreateTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.ReadUncommitted, CancellationToken cancellationToken = default);
}
