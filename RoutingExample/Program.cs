using RoutingExample.CustomConstraints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRouting(options =>
{
    options.ConstraintMap.Add("months",typeof(MonthsCustomConstraint));
});

var app = builder.Build();

// 启用路由
app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.Map("sales-report/{year:int:min(1900)}/{month:months}", async context =>
    {
        var year = context.Request.RouteValues["year"];
        var month = context.Request.RouteValues["month"];
        await context.Response.WriteAsync($"In sales-report: year {year}, month {month}");
    });
});

app.Run(async context =>
{
    await context.Response.WriteAsync($"Request received at {context.Request.Path}");
});

app.Run();