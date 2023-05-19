using MediatR;

namespace ElectronicVoting.API.Handler.Command
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
            throw new NotImplementedException();
        }
    }
}
