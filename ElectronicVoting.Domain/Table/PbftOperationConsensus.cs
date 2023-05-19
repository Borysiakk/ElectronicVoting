using ElectronicVoting.Domain.Enum;

namespace ElectronicVoting.Domain.Table;

public class PbftOperationConsensus :BaseEntity
{
    public string Body { get; set; }
    public string TransactionId { get; set; }
    public PbftOperationStatus Status { get; set; }
    public PbftOperationType Operations { get; set; }
}