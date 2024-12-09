var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// app.MapGet("/", () => "Hello World!");

app.Run(async (HttpContext context) =>
{
    context.Response.Headers["Content-Type"] = "text/html";
    if (context.Request.Headers.ContainsKey("AuthorizationKey"))
    {
        var authorizationKey = context.Request.Headers["AuthorizationKey"];
        await context.Response.WriteAsync($"<p>{authorizationKey}</p>");
    }
});

app.Run();
