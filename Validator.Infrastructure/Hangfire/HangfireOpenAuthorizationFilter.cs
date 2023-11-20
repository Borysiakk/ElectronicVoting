using Hangfire.Annotations;
using Hangfire.Dashboard;

namespace Validator.Infrastructure.Hangfire
{
    public class HangfireOpenAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize([NotNull] DashboardContext context)
        {
            return true;
        }
    }
}
