using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using CRUDWithFluxor;
using CRUDWithFluxor.Services;
using Fluxor;
using Fluxor.Blazor.Web.ReduxDevTools;
using Blazored.LocalStorage;
using Serilog;

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
    .MinimumLevel.Information()
    .WriteTo.BrowserConsole()
    .CreateLogger();

Log.Information("Application Started!");


// services 

builder.Services.AddScoped<UserService>();
// builder.Services.AddScoped<SnippetService>();
builder.Services.AddScoped<LocalStorageService>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped(sp => 
    new HttpClient { BaseAddress = new Uri("http://localhost:5185/users") });

await builder.Build().RunAsync();
