using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApp.MVC.Filters;

//I{Stage}Filter -> IStageExecuted & IStageExecuting
//IAsync{Stage}Filter ->IStageExecutionAcsync

//usually cache, cookies, potentially modify headers. 
//Usually async OR sync but not both
public class CustomResourceFilter : Attribute,  IAsyncResourceFilter, IResourceFilter
{
    public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
    {
        var userAgent = context.HttpContext.Request.Headers["User-Agent"].ToString();
        if (userAgent.Contains("Chrome"))
        {
            context.HttpContext.Response.Headers.Append("IsChromeUser", true.ToString());
        }
        await next();
    }

    //after auth filters
    //before method execution, action filters
    //before exceptions
    // before results
    public void OnResourceExecuting(ResourceExecutingContext context)
    {
        context.HttpContext.Response.Cookies.Append("LastVisit", DateTime.Now.ToString("O"));

    }

    //after method execution, action filters
    //after exceptions
    //after results
    public void OnResourceExecuted(ResourceExecutedContext context)
    {

    }
}