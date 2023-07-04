using MediatR;

namespace Validator.Domain.Handler.Command.Consensu.ChangeLeader;

public class ElectionPreparation :IRequest
{
    public string TransactionId { get; set; }
}
