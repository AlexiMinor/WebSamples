using System.Globalization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApp.MVC.Filters;

public class CheckParametersAttribute : Attribute, IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (context.ActionArguments.ContainsKey("number"))
        {
            if (!context.ActionArguments["number"].ToString().Equals(context.HttpContext.Request.Query["number"]))
            {
                var prepValue = context.HttpContext.Request.Query["number"].ToString().Replace(",", ".");
                if (double.TryParse(prepValue, NumberStyles.Any,
                        CultureInfo.InvariantCulture, out var correctInput))
                {
                    context.ActionArguments["number"] = correctInput;
                }
            }
        }
        await next();
    }
}