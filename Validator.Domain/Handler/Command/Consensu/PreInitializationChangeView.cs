using MediatR;
namespace Validator.Domain.Handler.Command.Consensu;

public class PreInitializationChangeView : IRequest
{
    public int Round { get; set; }
    public string TransactionId { get; set; }
}
