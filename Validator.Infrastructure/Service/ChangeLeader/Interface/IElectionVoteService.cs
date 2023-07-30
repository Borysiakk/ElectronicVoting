namespace Validator.Infrastructure.Service.ChangeLeader.Interface;


public interface IVoteChangeLeaderService
{
    Task<bool> IsVoteCountGreaterThanThreshold(string electionId, Int64 vote, CancellationToken cancellationToken);
}
public interface IPreVoteChangeLeaderService
{
    Task<bool> IsVoteCountGreaterThanThreshold(string preElectionId, bool decision, CancellationToken cancellationToken);
}

