using CRUDExample.Filters.ActionFilters;
using CRUDExample.StartupExtensions;
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


builder.Services.ConfigureServices(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    // app.UseExceptionHandlingMiddleware();
}

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