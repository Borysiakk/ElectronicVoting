using MediatR;

namespace ElectronicVoting.Validator.Domain.Handler.Command.Consensu;
public class PrePrepare : IRequest
{
    public long Voice { get; set; }
}

