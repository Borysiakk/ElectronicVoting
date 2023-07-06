using ElectronicVoting.Infrastructure.Repository;
using MediatR;
using Validator.Domain.Handler.Command.Consensu.ChangeLeader;
using Validator.Domain.Table.ChangeView;
using Validator.Infrastructure.Repository;

namespace Validator.API.Handler.Command.PbftConsensus.ChangeLeader
{
    public class PreElectionVoteRecordHandler : IRequestHandler<PreElectionVoteRecord>
    {

        private readonly ApproverRepository _approverRepository;
        private readonly PreElectionVoteRepository _preElectionVoteRepository;

        public PreElectionVoteRecordHandler(ApproverRepository approverRepository, PreElectionVoteRepository preElectionVoteRepository)
        {
            _approverRepository = approverRepository;
            _preElectionVoteRepository = preElectionVoteRepository;
        }

        public async Task Handle(PreElectionVoteRecord request, CancellationToken cancellationToken)
        {
            Console.WriteLine("PreElectionVoteRecord");
            var user = Environment.GetEnvironmentVariable("CONTAINER_NAME");
            var approver = await _approverRepository.GetByName(user, cancellationToken);

            PreElectionVote electionVote = new PreElectionVote
            {
                Decision = true,
                Round = request.Round,
                ApproverId = request.ApproverId,
                TransactionId = request.TransactionId
            };

            await _preElectionVoteRepository.AddAsync(electionVote, cancellationToken);
            await _preElectionVoteRepository.SaveAsync(cancellationToken);

        }
    }
}
