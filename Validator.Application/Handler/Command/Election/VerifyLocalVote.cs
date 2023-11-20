using MediatR;
using Validator.Application.Service.Election;
using Validator.Domain.Model.Request;

namespace Validator.Application.Handler.Command.Election;

public record VerifyLocalVote: IRequest
{
    public VoteRequest Vote { get; set; }
    public string SessionElectionId { get; set; }

    public VerifyLocalVote(VoteRequest vote, string sessionElectionId)
    {
        Vote = vote;
        SessionElectionId = sessionElectionId;
    }
}

public class VerifyLocalVoteHandler : IRequestHandler<VerifyLocalVote>
{
    private readonly IElectionService _electionService;
    private readonly IProofOfKnowledgeService _proofOfKnowledgeService;

    public VerifyLocalVoteHandler(IElectionService electionService, IProofOfKnowledgeService proofOfKnowledgeService)
    {
        _electionService = electionService;
        _proofOfKnowledgeService = proofOfKnowledgeService;
    }

    public async Task Handle(VerifyLocalVote request, CancellationToken cancellationToken)
    {
        try
        {
            var requestProofOfKnowledge = new ProofOfKnowledgeRequest(request.Vote, request.SessionElectionId);
            var resultVerifyVote = await _proofOfKnowledgeService.VerifyVote(requestProofOfKnowledge, cancellationToken);
            var finalizeLocalVote = new FinalizeLocalVote(resultVerifyVote.Result, resultVerifyVote.Hash, resultVerifyVote.SessionElectionId);

            await _electionService.FinalizeLocalVotes(finalizeLocalVote, cancellationToken);
        }
        catch (Exception ex) 
        {
            throw;
        }
    }
}
