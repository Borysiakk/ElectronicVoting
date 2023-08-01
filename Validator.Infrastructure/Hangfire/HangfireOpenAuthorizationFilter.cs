using Hangfire.Dashboard;
namespace Validator.Infrastructure.Hangfire;

public class HangfireOpenAuthorizationFilter : IDashboardAuthorizationFilter
{
    public bool Authorize(DashboardContext context)
    {
        return true; // Zawsze zwracaj true
    }
}
