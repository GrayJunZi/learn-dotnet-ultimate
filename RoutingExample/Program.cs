var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// 启用路由
app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.Map("employee/profile/{EmployeeName:minlength(3):maxlength(7)}", async context =>
    {
        var employeename = context.Request.RouteValues["employeename"];
        await context.Response.WriteAsync($"In Employee: {employeename}");
    });

    endpoints.Map("sales-report/{year:int:min(1900)}/{month:regex(^(apr|jul|oct|jan)$)}", async context =>
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