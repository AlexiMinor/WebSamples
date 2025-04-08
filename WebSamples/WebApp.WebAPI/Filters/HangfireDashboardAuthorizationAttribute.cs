using Hangfire.Dashboard;

namespace WebApp.WebAPI.Filters;

public class HangfireDashboardAuthorizationAttribute : IDashboardAuthorizationFilter
{
    private readonly string _roleName;
    public HangfireDashboardAuthorizationAttribute(string roleName)
    {
        _roleName = roleName;
    }


    public bool Authorize(DashboardContext context)
    {
        var httpContext = context.GetHttpContext();
        var user = httpContext.User;
        return user.IsInRole(_roleName);
    }
}