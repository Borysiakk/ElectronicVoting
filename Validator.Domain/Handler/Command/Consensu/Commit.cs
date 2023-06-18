using MediatR;

namespace Validator.Domain.Handler.Command.Consensu;
public class Commit : IRequest
{
    public byte[] Hash { get; set; }
    public string TransactionId { get; set; }
}
