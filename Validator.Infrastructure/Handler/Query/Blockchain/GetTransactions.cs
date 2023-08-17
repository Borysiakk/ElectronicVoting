
using MediatR;
using Validator.Domain.Table;
using Validator.Infrastructure.Repository.Blockchain;

namespace Validator.Infrastructure.Handler.Query.Blockchain;

public class GetTransactions : IRequest<IEnumerable<Transaction>>
{
    
}

public class GetTransactionsHandler : IRequestHandler<GetTransactions, IEnumerable<Transaction>>
{
    private readonly ITransactionRepository _transactionRepository;

    public GetTransactionsHandler(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task<IEnumerable<Transaction>> Handle(GetTransactions request, CancellationToken cancellationToken)
    {
        return await _transactionRepository.GetAll(cancellationToken);
    }
}
