using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApp.MVC.Filters;

public class CustomExceptionFilterAttribute : Attribute, IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        var exception = context.Exception;
        var actionName = context.ActionDescriptor.DisplayName;
        var exceptionMessage = exception.Message;
        var stackTrace = exception.StackTrace;

        context.Result = new ContentResult
        {
            Content = $"An error occurred in the {actionName} action of the controller. The error message is: {exceptionMessage}. {Environment.NewLine}The stack trace is: {stackTrace}",
            StatusCode = 500
        };
        context.ExceptionHandled = true;
    }
}