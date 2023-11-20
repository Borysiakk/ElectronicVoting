using MediatR;

namespace Validator.Application.Handler.Command.ChangeLeader;

public class PreElectionPreparation :IRequest
{

}

public class PreElectionPreparationHandler : IRequestHandler<PreElectionPreparation>
{
    public Task Handle(PreElectionPreparation request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
