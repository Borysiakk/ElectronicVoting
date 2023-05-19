using MediatR;
using ElectronicVoting.Domain.Table;
using ElectronicVoting.Infrastructure.Repository;

namespace ElectronicVoting.API.Handler.Command.Transaction
{
    public class AddRegisteredTransaction : IRequest
    {
        public string TransactionId { get; set; }
    }

    public class AddRegisteredTransactionHandler : IRequestHandler<AddRegisteredTransaction>
    {
        private readonly TransactionRepository _transactionRepository;

        public AddRegisteredTransactionHandler(TransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<Unit> Handle(AddRegisteredTransaction request, CancellationToken cancellationToken)
        {
            var transaction = new RegisteredTransaction
            {
                TransactionId = request.TransactionId
            };

            await _transactionRepository.AddAsync(transaction, cancellationToken);
            await _transactionRepository.SaveAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
