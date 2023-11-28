using Main.Domain.Dto;
using Main.Infrastructure.Repository;
using MediatR;

namespace Main.Application.Handler.Command;

public class GetCandidates :IRequest<CandidateDto>
{

}

public class GetCandidatesHandler : IRequestHandler<GetCandidates, IEnumerable<CandidateDto>>
{
    private readonly ICandidateRepository _candidateRepository;

    public GetCandidatesHandler(ICandidateRepository candidateRepository)
    {
        _candidateRepository = candidateRepository;
    }

    public async Task<IEnumerable<CandidateDto>> Handle(GetCandidates request, CancellationToken cancellationToken)
    {
        var candidates = await _candidateRepository.GetAll(cancellationToken);

        return candidates.Select(x => new CandidateDto
        {
            CandidateId = x.CandidateId,
            Name = x.Name,
            Value = x.Value,
        });
    }
}
