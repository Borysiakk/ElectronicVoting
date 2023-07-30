namespace Validator.Infrastructure.Service.ChangeLeader;
public interface IPreChangeLeaderService
{
    Task<bool> CheckLeadershipChangeReason(CancellationToken cancellationToken);
}
public class PreChangeLeaderService : IPreChangeLeaderService
{
    public async Task<bool> CheckLeadershipChangeReason(CancellationToken cancellationToken)
    {
        return true;
    }
}
