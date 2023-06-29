namespace Validator.Domain;
public static class Routes
{
    public const string Reply = "api/PbftConsensus/Reply";
    public const string Commit = "api/PbftConsensus/Commit";
    public const string Prepare = "api/PbftConsensus/Prepare";
    public const string PrePrepare = "api/PbftConsensus/Pre-Prepare";

    public const string ElectionVoteRecord = "api/PbftConsensus/ChangeView/election-vote-record";
    public const string PreElectionVoteRecord = "api/PbftConsensus/ChangeView/pre-election-vote-record";
    public const string PreElectionPreparation = "api/PbftConsensus/ChangeView/pre-election-preparation";

    public const string TransactionRegister = "/api/Transaction/Add";
}
