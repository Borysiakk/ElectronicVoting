namespace ElectronicVoting.Validator.Consensus
{
    public enum Stage:int
    {
        PrePrepare = 0,
        Prepare = 1,
        Commit = 2,
        WriteBlock = 3,
    }
}