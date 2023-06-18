
using MediatR;

namespace Validator.Domain.Handler.Command.Consensu;
public class InitializationChangeView :IRequest
{
    public int Round { get; set; }
    public string TransactionId { get; set; }

    //Dodać powód zmiany walidatora głownego
}
