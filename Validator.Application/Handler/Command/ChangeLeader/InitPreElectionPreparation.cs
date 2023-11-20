using MediatR;

namespace Validator.Application.Handler.Command.ChangeLeader;

public class InitPreElectionPreparation :IRequest
{

}

public class InitPreElectionPreparationHandler : IRequestHandler<InitPreElectionPreparation>
{
    public Task Handle(InitPreElectionPreparation request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
