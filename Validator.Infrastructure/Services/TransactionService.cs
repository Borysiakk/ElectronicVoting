using Validator.Domain.Table.Blockchain;

namespace ElectronicVoting.Infrastructure.Services;

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
