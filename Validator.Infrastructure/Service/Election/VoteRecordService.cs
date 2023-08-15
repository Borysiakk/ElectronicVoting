using Validator.Domain;
using Validator.Domain.Table.Election;

namespace Validator.Infrastructure.Service.Election
{
    public interface IVoteRecordService
    {
        Task PublishVote(Int64 Vote ,string voteProcessId, CancellationToken cancellationToken);
    }

    public class VoteRecordService : IVoteRecordService
    {
        private readonly IApproverService _approverService;

        public VoteRecordService(IApproverService approverService)
        {
            _approverService = approverService;
        }

        public async Task PublishVote(Int64 Vote, string voteProcessId, CancellationToken cancellationToken)
        {
            var includeSender = true;
            var voteRecord = new VoteRecord(Vote ,voteProcessId);

            await _approverService.SendPostToApprovers(Routes.RegisterVote, voteRecord, includeSender,cancellationToken);
        }
    }
}
