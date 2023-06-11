using MediatR;
using ElectronicVoting.Infrastructure.Repository;
using Validator.Domain.Handler.Command;
using ElectronicVoting.Validator.Domain.Table;

namespace ElectronicVoting.API.Handler.Command.Transaction
{

    public class AddRegisteredTransactionHandler : IRequestHandler<AddRegisteredTransaction>
    {
        private readonly TransactionRepository _transactionRepository;

        public AddRegisteredTransactionHandler(TransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task Handle(AddRegisteredTransaction request, CancellationToken cancellationToken)
        {
            var transaction = new TransactionRegister
            {
                TransactionId = request.TransactionId
            };

            await _transactionRepository.AddAsync(transaction, cancellationToken);
            await _transactionRepository.SaveAsync(cancellationToken);
        }
    }
}
