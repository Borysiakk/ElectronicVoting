

using Validator.Domain.Queue.Consensus;

namespace Validator.Domain.Models.Queue.Consensus.ChangeView;

public class ItemBodyPreInitializationChangeView :ItemBody
{
    public int Round { get; set; }
}
