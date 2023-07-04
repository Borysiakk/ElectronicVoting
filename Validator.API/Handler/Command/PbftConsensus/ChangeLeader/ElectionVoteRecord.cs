using MediatR;

namespace Validator.API.Handler.Command.PbftConsensus.ChangeLeader
{
    public class ElectionVoteRecord :IRequest
    {
        public string TransactionId { get; set; }
        public Int64 ApproverId { get; set; }
        public Int64 SelectedApproverId { get; set; }
    }

    public class ElectionVoteRecordHandler : IRequestHandler<ElectionVoteRecord>
    {
        public async Task Handle(ElectionVoteRecord request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
