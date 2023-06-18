namespace Validator.Domain.Enum;
public enum PbftOperationType
{
    CommitInitializationChangeView,
    PreInitializationChangeView,
    InitializationChangeView,
    PrePrepare,
    Prepare,
    Commit,
    Reply
}
