using MediatR;
using Validator.Domain.Table.ChangeView;
using Validator.Infrastructure.Repository;

namespace Validator.API.Handler.Command.PbftConsensus.ChangeLeader
{
    public class ElectionVoteRecord :IRequest
    {
        public int Round { get; set; }
        public string TransactionId { get; set; }
        public Int64 ApproverId { get; set; }
        public Int64 SelectedApproverId { get; set; }
    }

    public class ElectionVoteRecordHandler : IRequestHandler<ElectionVoteRecord>
    {
        private readonly ElectionVoteRepository _electionVoteRepository;

        public ElectionVoteRecordHandler(ElectionVoteRepository electionVoteRepository)
        {
            _electionVoteRepository = electionVoteRepository;
        }

        public async Task Handle(ElectionVoteRecord request, CancellationToken cancellationToken)
        {
            ElectionVote electionVote = new ElectionVote()
            {
                Round = request.Round,
                ApproverId = request.ApproverId,
                SelectedApproverId = request.SelectedApproverId,
                TransactionId = request.TransactionId,
            };

            await _electionVoteRepository.AddAsync(electionVote, cancellationToken);
            await _electionVoteRepository.SaveAsync(cancellationToken);

        }
    }
}
