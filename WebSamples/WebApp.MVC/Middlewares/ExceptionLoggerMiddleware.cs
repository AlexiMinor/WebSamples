namespace WebApp.MVC.Middlewares;

public class ExceptionLoggerMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionLoggerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {

            var secretToken = context.Request.Query["w"];
            if (secretToken == "123456789")
            {
                context.Response.Headers.TryAdd("SomeSecretKnowledge", "You won smth(technically not)");
            }

            try
            {
                await _next.Invoke(context);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync("An error occurred. Please try again later.");
        }
    }
}