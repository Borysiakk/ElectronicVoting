using MediatR;
using Validator.Domain;
using Validator.Domain.Models.Request;
using Validator.Infrastructure.Service;
using Validator.Infrastructure.Service.Election;
namespace Validator.Infrastructure.Handler.Command.Election;

public class ValidateLocalVote : IRequest
{
    public Int64 Vote { get; set; }
    public string VoteProcessId { get; set; }
    public ValidateLocalVote(Int64 vote, string voteProcessId)
    {
        Vote = vote;
        VoteProcessId = voteProcessId;
    }
}

public class ValidateLocalVoteHandler : IRequestHandler<ValidateLocalVote>
{
    private readonly IApproverService _approverService;
    private readonly IProofOfKnowledgeService _proofOfKnowledgeService;

    public ValidateLocalVoteHandler(IApproverService approverService, IProofOfKnowledgeService proofOfKnowledgeService)
    {
        _approverService = approverService;
        _proofOfKnowledgeService = proofOfKnowledgeService;
    }

    public async Task Handle(ValidateLocalVote request, CancellationToken cancellationToken)
    {
        var includeSender = true;
        var requestProofOfKnowledge = new ProofOfKnowledgeRequest()
        {
            Vote = request.Vote,
            VoteProcessId = request.VoteProcessId,
        };

        var resultValidation = await _proofOfKnowledgeService.Validation(requestProofOfKnowledge, cancellationToken);
        var finalizeVoting = new FinalizeLocalVoting()
        {
            Hash = resultValidation.Hash,
            VoteProcessId = request.VoteProcessId
        };

        await _approverService.SendPostToApprovers(Routes.FinalizeLocalVoting, finalizeVoting, includeSender, cancellationToken);
    }
}
