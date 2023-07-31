using Validator.Domain.Models.Request;
using Validator.Domain.Models.Result;
using Validator.Infrastructure.Helper;

namespace Validator.Infrastructure.Service.Election;

public interface IProofOfKnowledgeService
{
    public Task<ProofOfKnowledgeResult> Validation(ProofOfKnowledgeRequest request, CancellationToken cancellationToken);
}

public class ProofOfKnowledgeService : IProofOfKnowledgeService
{
    public async Task<ProofOfKnowledgeResult> Validation(ProofOfKnowledgeRequest request, CancellationToken cancellationToken)
    {
        await Task.Delay(TimeSpan.FromSeconds(2));

        return new ProofOfKnowledgeResult()
        {
            VoteProcessId = request.VoteProcessId,
            Hash = HashHelper.ComputeHash(request),
        };
    }
}
