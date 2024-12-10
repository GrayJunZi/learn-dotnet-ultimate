namespace MiddlewareExample.CustomMiddleware;

public class HelloCustomMiddleware
{
    private readonly RequestDelegate _next;
    public HelloCustomMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        await context.Response.WriteAsync("Hello Custom Middleware - Starts\n");
        await _next(context);
        await context.Response.WriteAsync("Hello Custom Middleware - Ends\n");
    }
}

public static class HelloCustomMiddlewareExtensions
{
    public static IApplicationBuilder UseHelloCustomMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<HelloCustomMiddleware>();
        return app;
    }
}