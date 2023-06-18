using MediatR;


namespace Validator.Domain.Handler.Command.Consensu;
public class Prepare : IRequest
{
    public long Voice { get; set; }
    public string TransactionId { get; set; }
}

