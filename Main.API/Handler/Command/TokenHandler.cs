using Main.Infrastructure.Repository;
using MediatR;

namespace Main.API.Handler.Command
{
    public class Token : IRequest
    {

    }

    public class TokenHandler : IRequestHandler<Token>
    {
        private readonly TokenRepository _tokenRepository;

        public TokenHandler(TokenRepository tokenRepository)
        {
            _tokenRepository = tokenRepository;
        }

        public async Task Handle(Token request, CancellationToken cancellationToken)
        {
            
        }
    }
}
