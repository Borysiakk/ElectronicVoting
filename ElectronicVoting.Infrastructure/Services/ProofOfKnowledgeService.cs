using ElectronicVoting.Domain.Contract.Request;
using ElectronicVoting.Domain.Contract.Result;
using ElectronicVoting.Infrastructure.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicVoting.Infrastructure.Services
{
    public interface IProofOfKnowledgeService
    {
        public Task<ProofOfKnowledgeResult> Validation(ProofOfKnowledgeRequest request);
    }
    public class ProofOfKnowledgeService : IProofOfKnowledgeService
    {
        public async Task<ProofOfKnowledgeResult> Validation(ProofOfKnowledgeRequest request)
        {
            await Task.Delay(TimeSpan.FromSeconds(10));
            return new ProofOfKnowledgeResult() { 
                TransactionId = request.TransactionId,
                Hash = HashHelper.ComputeHash(request),
            };

        }
    }
}
