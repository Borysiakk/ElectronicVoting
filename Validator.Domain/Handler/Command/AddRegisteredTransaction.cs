using MediatR;

namespace Validator.Domain.Handler.Command;
public class AddRegisteredTransaction : IRequest
{
    public string TransactionId { get; set; }
}
