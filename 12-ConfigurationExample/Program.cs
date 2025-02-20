using ConfigurationExample;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
/*
app.Map("/", async context =>
{
    // 忽略大小写
    await context.Response.WriteAsync(app.Configuration["myKEY"] + "\n");

    // 忽略大小写
    await context.Response.WriteAsync(app.Configuration.GetValue<string>("MYkey") + "\n");

    await context.Response.WriteAsync(app.Configuration.GetValue<int>("x") + "\n");
    await context.Response.WriteAsync(app.Configuration.GetValue<int>("y", 10) + "\n");
});
*/

builder.Services.Configure<ApiOptions>(builder.Configuration.GetSection("API"));

builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    config.AddJsonFile("CustomConfig.json", optional: true, reloadOnChange: true);
});

app.MapControllers();


app.Run();