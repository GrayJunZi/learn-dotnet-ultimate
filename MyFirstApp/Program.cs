var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// app.MapGet("/", () => "Hello World!");

app.Run(async (HttpContext context) =>
{
    context.Response.Headers["Content-Type"] = "text/html";
    if (context.Request.Method == "GET")
    {
        if (context.Request.Query.ContainsKey("id"))
        {
            var id = context.Request.Query["id"];
            await context.Response.WriteAsync($"<p>{id}</p>");
        }
    }
});

app.Run();
