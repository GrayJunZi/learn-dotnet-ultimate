var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// 启用路由
app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.Map("products/details/{id:int?}", async context =>
    {
        var id = context.Request.RouteValues["ID"];
        if (id != null)
        {
            await context.Response.WriteAsync($"In product: Id {id}");
        }
        else
        {
            await context.Response.WriteAsync($"In product: Id is not supplied");
        }
    });

    endpoints.Map("daily-digest-report/{reportdate:datetime}", async context =>
    {
        var reportdate = context.Request.RouteValues["reportdate"];
        await context.Response.WriteAsync($"In daily-digest-report: reportdate {reportdate}");
    });
});

app.Run(async context =>
{
    await context.Response.WriteAsync($"Request received at {context.Request.Path}");
});

app.Run();