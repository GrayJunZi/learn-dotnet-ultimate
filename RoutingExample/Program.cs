var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Use(async (context, next) =>
{
    Microsoft.AspNetCore.Http.Endpoint? endpoint = context.GetEndpoint();
    await context.Response.WriteAsync($"UseRouting Before is null {endpoint == null}\n");
    await next(context);
});

// 启用路由
app.UseRouting();

app.Use(async (context, next) =>
{
    Microsoft.AspNetCore.Http.Endpoint? endpoint = context.GetEndpoint();
    await context.Response.WriteAsync($"UseRouting After is null {endpoint == null}, DisplayName: {endpoint?.DisplayName}\n");
    await next(context);
});

app.Map("map1", async (context) =>
{
    await context.Response.WriteAsync($"In map1");
});

app.Run();