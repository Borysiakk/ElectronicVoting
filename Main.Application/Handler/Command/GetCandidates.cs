using Main.Domain.Dto;
using MediatR;

namespace Main.Application.Handler.Command;

public class GetCandidates :IRequest<CandidateDto>
{

}

public class GetCandidatesHandler : IRequestHandler<GetCandidates, CandidateDto>
{
    private readonly ICandidateRepository _candidateRepository;
    public Task<CandidateDto> Handle(GetCandidates request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
