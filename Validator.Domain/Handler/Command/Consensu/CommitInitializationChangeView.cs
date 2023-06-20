using MediatR;

namespace Validator.Domain.Handler.Command.Consensu;
public class CommitInitializationChangeView :IRequest
{
    public int ApproverId { get; set; }
    public int Round { get; set; }
    public bool Decision { get; set; }
    public string TransactionId { get; set; }
}
