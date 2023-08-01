using MediatR;
using Validator.Domain.Table.Election;
using Validator.Infrastructure.Repository.Election;

namespace Validator.Infrastructure.Handler.Command.Election
{
    public class RegisterVote :IRequest
    {
        public string VoteProcessId { get; set; }
        public RegisterVote(string voteProcessId)
        {
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
            var voteRecord = new VoteRecord(request.VoteProcessId);
            await _voteRecordRepository.Add(voteRecord, cancellationToken);
        }
    }
}
