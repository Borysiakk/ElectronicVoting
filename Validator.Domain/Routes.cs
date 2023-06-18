namespace Validator.Domain;
public static class Routes
{
    public const string Reply = "api/PbftConsensus/Reply";
    public const string Commit = "api/PbftConsensus/Commit";
    public const string Prepare = "api/PbftConsensus/Prepare";
    public const string PrePrepare = "api/PbftConsensus/Pre-Prepare";

    public const string InitializationChangeView = "api/PbftConsensus/Initialization-ChangeView";
    public const string CommitInitializationChangeView = "api/PbftConsensus/Commit-Initialization-ChangeView";

    public const string TransactionRegister = "/api/Transaction/Add";
}
