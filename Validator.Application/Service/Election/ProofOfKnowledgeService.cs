using Validator.Domain.Model.Request;
using Validator.Domain.Model.Result;
using Validator.Infrastructure.Helper;

namespace Validator.Application.Service.Election;

public interface IProofOfKnowledgeService
{
    Task<ProofOfKnowledgeResult> VerifyVote(ProofOfKnowledgeRequest request, CancellationToken cancellationToken);
}
public class ProofOfKnowledgeService : IProofOfKnowledgeService
{
    public async Task<ProofOfKnowledgeResult> VerifyVote(ProofOfKnowledgeRequest request, CancellationToken cancellationToken)
    {
        await Task.Delay(TimeSpan.FromSeconds(2), cancellationToken);

        return new ProofOfKnowledgeResult()
        { 
            Result = true,
            Hash = HashHelper.ComputeHash(request),
            SessionElectionId = request.SessionElectionId
        };
    }
}