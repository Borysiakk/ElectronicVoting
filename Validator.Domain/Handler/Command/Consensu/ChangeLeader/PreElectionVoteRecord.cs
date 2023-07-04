using MediatR;

namespace Validator.Domain.Handler.Command.Consensu.ChangeLeader;

public class PreElectionVoteRecord : IRequest
{
    public int Round { get; set; }
    public bool Decision { get; set; }
    public Int64 ApproverId { get; set; }
    public string TransactionId { get; set; }
}