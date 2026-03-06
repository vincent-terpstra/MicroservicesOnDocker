namespace CommandsService.Middleware;

public static class ExceptionHandlingMiddleware
{
    public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder app)
    {
        app.Use(HandleExceptions);
        return app;
    }

    private static async Task HandleExceptions(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (InvalidOperationException ex) when (ex.Message == "Sequence contains no elements")
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            await context.Response.WriteAsync($"Unable to find resource: {context.Request.Path}");
        }
    }
}