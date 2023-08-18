
using Main.Domain.Table;
using Main.Infrastructure.Repository;
using MediatR;

namespace Main.Infrastructure.Handler.Command;

public record SetCurrentLeader(string Name, string Adress) :IRequest;


public class SetCurrentLeaderHandler : IRequestHandler<SetCurrentLeader>
{
    private readonly ICurrentLeaderRepository _currentLeaderRepository;

    public SetCurrentLeaderHandler(ICurrentLeaderRepository currentLeaderRepository)
    {
        _currentLeaderRepository = currentLeaderRepository;
    }

    public async Task Handle(SetCurrentLeader request, CancellationToken cancellationToken)
    {
        var currentLeader = new CurrentLeader(request.Name,request.Adress);

        await _currentLeaderRepository.Add(currentLeader, cancellationToken);
    }
}