using ElectronicVoting.Domain.Enum;
using ElectronicVoting.Domain.Models.Queue.Consensus;
using ElectronicVoting.Domain.Table;
using ElectronicVoting.Infrastructure.Helper;
using MediatR;

namespace ElectronicVoting.API.Handler.Command.PbftConsensus
{
    public class Commit : IRequest
    {
        public byte[] Hash { get; set; }
        public string TransactionId { get; set; }
    }

    public class CommitHandler : IRequestHandler<Commit>
    {
        public async Task<Unit> Handle(Commit request, CancellationToken cancellationToken)
        {
            var item = new ItemBodyCommit()
            {
                Hash = request.Hash,
                TransactionId = request.TransactionId,
            };

            var operation = new PbftOperationConsensus()
            {
                Body = item.SerializeJson(),
                Status = PbftOperationStatus.NotReady,
                Operations = PbftOperationType.Commit,
            };
            
            return Unit.Value;
        }
    }
}
