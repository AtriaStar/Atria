using Frontend;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Options;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services
    .AddScoped<LoginManager>()
    .AddSingleton<CookieHandler>()
    .AddHttpClient(Options.DefaultName, client => client.BaseAddress =
        new(builder.Configuration["BaseAddress"] ?? throw new InvalidOperationException("No backend address provided")))
    .AddHttpMessageHandler<CookieHandler>();
await builder.Build().RunAsync();
