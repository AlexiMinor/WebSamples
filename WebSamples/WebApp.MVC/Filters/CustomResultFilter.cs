using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApp.MVC.Filters;

public class CustomResultFilter : Attribute, IResultFilter
{
    public void OnResultExecuting(ResultExecutingContext context)
    {
        //set cookies. headers, etc

    }

    public void OnResultExecuted(ResultExecutedContext context)
    {
        context.Canceled = true;
    }
}