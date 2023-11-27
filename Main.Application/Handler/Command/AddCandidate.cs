using MediatR;

namespace Main.Application.Handler.Command;

public class AddCandidate : IRequest
{
    public string Name { get; set; }
}

public class AddCandidateHandler : IRequestHandler<AddCandidate>
{
    public Task Handle(AddCandidate request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
