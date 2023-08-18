using Main.Dto;
using Main.Infrastructure.Repository;
using MediatR;

namespace Main.Infrastructure.Handler;

public record GetCurrentLeader() :IRequest<LeaderDto>;

public class GetCurrentLeaderHandler : IRequestHandler<GetCurrentLeader, LeaderDto>
{
    private readonly ICurrentLeaderRepository _currentLeaderRepository;

    public GetCurrentLeaderHandler(ICurrentLeaderRepository currentLeaderRepository)
    {
        _currentLeaderRepository = currentLeaderRepository;
    }

    public async Task<LeaderDto> Handle(GetCurrentLeader request, CancellationToken cancellationToken)
    {
        var leader = await _currentLeaderRepository.GetLastLeader(cancellationToken);

        return new LeaderDto("", "");  
    }
}
