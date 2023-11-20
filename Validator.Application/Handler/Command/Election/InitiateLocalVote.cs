using MediatR;
using Validator.Application.Service.Election;
using Validator.Domain.Model.Request;


namespace Validator.Application.Handler.Command.Election;

public record InitiateLocalVote : IRequest
{
    public required VoteRequest Vote { get; set; }
}

public class InitiateLocalVoteHandler : IRequestHandler<InitiateLocalVote>
{
    private readonly IElectionService _electionService;
    private readonly IApproverService _approverService;

    public InitiateLocalVoteHandler(IApproverService approverService, IElectionService electionService)
    {
        _approverService = approverService;
        _electionService = electionService;
    }

    public async Task Handle(InitiateLocalVote request, CancellationToken cancellationToken)
    {
        try
        { 
            var sessionElectionId = Guid.NewGuid().ToString();

            var registerVote = new RegisterVote(request.Vote.Vote,sessionElectionId);
            await _electionService.RegisterVotes(registerVote, cancellationToken);

            var verifyLocalVote = new VerifyLocalVote(request.Vote, sessionElectionId);
            await _electionService.VerifyLocalVotes(verifyLocalVote, cancellationToken);
        }
        catch (Exception ex) 
        {
            throw;
        }
    }
}
