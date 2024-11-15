using System.Diagnostics.CodeAnalysis;
using Hangfire.Dashboard;

namespace BudgetManagementAPI.Services.Utils
{
    public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize([NotNull] DashboardContext context)
        {
            return context.GetHttpContext().User.Identity.IsAuthenticated;
        }
    }
}
