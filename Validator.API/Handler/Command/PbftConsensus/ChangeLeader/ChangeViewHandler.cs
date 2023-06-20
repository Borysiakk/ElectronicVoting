using MediatR;
using Validator.Domain.Handler.Command.Consensu.ChangeLeader;

namespace Validator.API.Handler.Command.PbftConsensus.ChangeLeader;

public class ChangeViewHandler : IRequestHandler<ChangeView>
{
    public Task Handle(ChangeView request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
