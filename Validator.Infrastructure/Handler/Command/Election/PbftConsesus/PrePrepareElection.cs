using MediatR;

namespace Validator.Infrastructure.Handler.Command.Election.PbftConsesus;

public class PrePrepareElection :IRequest
{
    public Int64 Voice { get; set; }
    public string VoteProcessId { get; set; }
}

public class PrePrepareElectionHandler : IRequestHandler<PrePrepareElection>
{
    public async Task Handle(PrePrepareElection request, CancellationToken cancellationToken)
    {
        
    }
}
