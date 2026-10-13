// using WASM


using Blazored.LocalStorage;
using CRUDWithFluxor;
using CRUDWithFluxor.Services;
using Fluxor;
using Fluxor.Blazor.Web.ReduxDevTools;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Extensions.Logging;



// this alone is the key indicator for WASM
var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");


// adding local storage //
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddDevExpressBlazor();
builder.RootComponents.Add<HeadOutlet>("head::after");

// adding fluxor service 

builder.Services.AddFluxor(options =>
{
    options.ScanAssemblies(typeof(Program).Assembly);
    options.UseReduxDevTools();
});

// adding serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .Enrich.WithProperty("InstanceId", Guid.NewGuid().ToString("n"))
    .WriteTo.BrowserHttp($"{builder.Configuration.GetValue<string>("ApiBaseAddress")}ingest", controlLevelSwitch: new LoggingLevelSwitch(LogEventLevel.Error))
    .WriteTo.BrowserConsole(levelSwitch: new LoggingLevelSwitch(LogEventLevel.Warning))
    .CreateLogger();

builder.Logging
       //.ClearProviders() // Commented this as it affects default global blazor-error-ui from getting rendered.
       .AddProvider(new SerilogLoggerProvider());

Log.Information("Application Started!");


// services 

builder.Services.AddScoped<UserService>();
// builder.Services.AddScoped<SnippetService>();
builder.Services.AddScoped<LocalStorageService>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped(sp => 
    new HttpClient { BaseAddress = new Uri("http://localhost:5185/users") 
    });

builder.Services.AddScoped<ErrorLogService>();

await builder.Build().RunAsync();
