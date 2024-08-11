using Hangfire.Dashboard;
using System.Diagnostics.CodeAnalysis;

namespace BudgetManagementAPI.Utils
{
    public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize([NotNull] DashboardContext context)
        {
            return context.GetHttpContext().User.Identity.IsAuthenticated;
        }
    }
}
