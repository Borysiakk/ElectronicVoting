using MediatR;

namespace Validator.Domain.Handler.Command.Consensu.ChangeLeader;
public class ChangeView : IRequest
{
    public int Round { get; set; }
    public string TransactionId { get; set; }
}
