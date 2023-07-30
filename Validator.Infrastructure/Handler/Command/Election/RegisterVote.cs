using MediatR;
using Validator.Domain.Table.Election;
using Validator.Infrastructure.Repository.Election;

namespace Validator.Infrastructure.Handler.Command.Election
{
    public class RegisterVote :IRequest
    {
        public string VoteProcessId { get; set; }
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
            VoteRecord voteRecord = new VoteRecord()
            {
                VoteProcessId = request.VoteProcessId,
            };

            await _voteRecordRepository.Add(voteRecord, cancellationToken);
        }
    }
}
