using Validator.Domain.Table;
using Validator.Infrastructure.Repository.Blockchain;

namespace Validator.Infrastructure.Service.Blockchain;

public interface ITransactionService
{

}

public class TransactionService : ITransactionService
{

    private readonly IBlockRepository _blockRepository;

    public TransactionService(IBlockRepository blockRepository)
    {
        _blockRepository = blockRepository;
    }

}
