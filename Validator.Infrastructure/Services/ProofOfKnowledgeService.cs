﻿using ElectronicVoting.Validator.Domain.Contract.Request;
using ElectronicVoting.Validator.Domain.Contract.Result;
using ElectronicVoting.Validator.Infrastructure.Helper;

namespace ElectronicVoting.Infrastructure.Services;
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