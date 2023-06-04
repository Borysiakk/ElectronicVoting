namespace ElectronicVoting.Common
{
    public static class Routes
    {
        public const string Reply = "api/PbftConsensus/Reply";
        public const string Commit = "api/PbftConsensus/Commit";
        public const string Prepare = "api/PbftConsensus/Prepare";
        public const string PrePrepare = "api/PbftConsensus/Prepare";

        public const string TransactionRegister = "/api/Transaction/Add";
    }
}
