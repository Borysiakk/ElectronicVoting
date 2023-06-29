using MediatR;

namespace Validator.API.Handler.Command.PbftConsensus.ChangeLeader
{
    public class ElectionVoteRecord :IRequest
    {

    }

    public class ElectionVoteRecordHandler : IRequestHandler<ElectionVoteRecord>
    {
        public async Task Handle(ElectionVoteRecord request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
