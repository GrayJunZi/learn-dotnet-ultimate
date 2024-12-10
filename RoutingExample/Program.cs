var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// 启用路由
app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.Map("map1", async (context) =>
    {
        await context.Response.WriteAsync("In map1");
    });

    endpoints.Map("map2", async (context) =>
    {
        await context.Response.WriteAsync("In map2");
    });

    endpoints.MapGet("map_get", async (context) =>
    {
        await context.Response.WriteAsync("In map get");
    });

    endpoints.MapPost("map_post", async (context) =>
    {
        await context.Response.WriteAsync("In map post");
    });
});

app.Run(async context =>
{
    await context.Response.WriteAsync($"Request received at {context.Request.Path}");
});

app.Run();