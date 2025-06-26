using CRUDExample.Filters.ActionFilters;
using Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;
using RepositoryContracts;
using ServiceContracts;
using Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

/*
builder.Host.ConfigureLogging(loggingProvidder =>
{
    loggingProvidder.ClearProviders();
    loggingProvidder.AddConsole();
});

builder.Services.AddHttpLogging(options =>
{
    options.LoggingFields = HttpLoggingFields.RequestPropertiesAndHeaders
                            | HttpLoggingFields.ResponsePropertiesAndHeaders;
});
*/

builder.Host.UseSerilog((context, services, configuration) =>
{
    configuration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services);
});

builder.Services.AddControllersWithViews(options =>
{
    var logger = builder.Services.BuildServiceProvider()
        .GetService<ILogger<ResponseHeaderActionFilter>>();
    options.Filters.Add(new ResponseHeaderActionFilter(logger, "My-Key-From-Global", "My-Value-From-Global"));
});

builder.Services.AddScoped<ICountriesRepository, CountriesRepository>();
builder.Services.AddScoped<IPersonsRepository, PersonsRepository>();

builder.Services.AddScoped<ICountriesService, CountriesService>();
builder.Services.AddScoped<IPersonsService, PersonsService>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("CRUDExample")));

var app = builder.Build();

app.UseSerilogRequestLogging();

// app.UseRotativa();
Rotativa.AspNetCore.RotativaConfiguration.Setup("wwwroot", wkhtmltopdfRelativePath: "Rotativa");

/*
app.Logger.LogDebug("debug-message");
app.Logger.LogInformation("information-message");
app.Logger.LogWarning("warning-message");
app.Logger.LogError("error-message");
app.Logger.LogCritical("critical-message");

app.UseHttpLogging();
*/
app.UseStaticFiles();
app.UseRouting();
app.MapControllers();

app.Run();

public partial class Program
{
}