using MediatR;
using Validator.Domain.Table.Election;
using Validator.Infrastructure.Repository.Election;

namespace Validator.Infrastructure.Handler.Command.Election
{
    public class RegisterVote :IRequest
    {
        public Int64 Vote { get; set; }
        public string VoteProcessId { get; set; }
        public RegisterVote(Int64 vote, string voteProcessId)
        {
            Vote = vote;
            VoteProcessId = voteProcessId;
        }
    }

    public class RegisterVoteHandler : IRequestHandler<RegisterVote>
    {
        private readonly IVoteRecordRepository _voteRecordRepository;

        public RegisterVoteHandler(IVoteRecordRepository voteRecordRepository)
        {
            _voteRecordRepository = voteRecordRepository;
        }

        public async Task Handle(RegisterVote request, CancellationToken cancellationToken)
        {
            var voteRecord = new VoteRecord(request.Vote, request.VoteProcessId);
            await _voteRecordRepository.Add(voteRecord, cancellationToken);
        }
    }
}
