namespace Validator.Domain.Enum;
public enum PbftOperationType
{
    CommitInitializationChangeView,
    PreInitializationChangeView,
    InitializationChangeView,
    ChangeView,
    PrePrepare,
    Prepare,
    Commit,
    Reply
}
