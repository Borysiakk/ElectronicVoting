using MediatR;

namespace Validator.Domain.Handler.Command.Consensu;
public class PrePrepare : IRequest
{
    public long Voice { get; set; }
}

