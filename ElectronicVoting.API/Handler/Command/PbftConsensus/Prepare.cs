using MediatR;

namespace ElectronicVoting.API.Handler.Command
{
    public class Prepare : IRequest
    {
        public long Voice { get; set; }
        public string TransactionId { get; set; }
    }

    public class PrepareHandler : IRequestHandler<Prepare>
    {
        public async Task<Unit> Handle(Prepare request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
