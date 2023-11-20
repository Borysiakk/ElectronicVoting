using MediatR;
using Validator.Application.Service;
using Validator.Application.Service.Election;
using Validator.Domain.Table.Electronic;
using Validator.Infrastructure.Repository.Election;
using Validator.Infrastructure.Repository.Election.Base;

namespace Validator.Application.Handler.Command.Election
{
    public record FinalizeLocalVote : IRequest
    {
        public byte[] Hash { get; set; }
        public bool ResultVerifyVote { get; set; }
        public string SessionElectionId { get; set; }
        public FinalizeLocalVote(bool resultVerifyVote, byte[] hash, string sessionElectionId)
        {
            Hash = hash;
            ResultVerifyVote = resultVerifyVote;
            SessionElectionId = sessionElectionId;
        }
    }

    public class FinalizeLocalVoteHandler : IRequestHandler<FinalizeLocalVote>
    {
        private readonly IPendingVoteService _pendingVoteService;
        private readonly IPendingLocalVoteRepository _pendingLocalVoteRepository;

        public FinalizeLocalVoteHandler(IPendingVoteService pendingVoteService, IPendingLocalVoteRepository pendingLocalVoteRepository)
        {
            _pendingVoteService = pendingVoteService;
            _pendingLocalVoteRepository = pendingLocalVoteRepository;
        }

        public async Task Handle(FinalizeLocalVote request, CancellationToken cancellationToken)
        {
            var pendingLocalVote = new PendingLocalVote()
            {
                Hash = request.Hash,
                ResultVerifyVote = request.ResultVerifyVote,
                SessionElectionId = request.SessionElectionId,
            };

            await _pendingLocalVoteRepository.Add(pendingLocalVote, cancellationToken);
            await _pendingVoteService.CheckAndNotifyVoteLocalCompletion(request.SessionElectionId, request.ResultVerifyVote, request.Hash, cancellationToken);
        }
    }
}