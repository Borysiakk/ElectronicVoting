
using ElectronicVoting.Validator.Domain.Queue.Consensus;

namespace Validator.Domain.Models.Queue.Consensus;

public class ItemBodyPreInitializationChangeView :ItemBody
{
    public int Round { get; set; }
}
