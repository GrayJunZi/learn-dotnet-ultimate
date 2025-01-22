using StocksApp;
using StocksApp.ServiceContracts;
using StocksApp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddScoped<IPostService, PostService>();

builder.Services.Configure<TradingOptions>(builder.Configuration.GetSection("TradingOptions"));

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.MapControllers();

app.Run();