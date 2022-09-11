using Frontend;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Options;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var opt = builder.Configuration.Get<FrontendSettings>()
    ?? throw new InvalidOperationException("Not all necessary options are set");

builder.Services
    .AddSingleton(opt)
    .AddScoped<CookieHandler>()
    .AddHttpClient(Options.DefaultName, client => client.BaseAddress = new(opt.BaseAddress))
    .AddHttpMessageHandler<CookieHandler>();

await builder.Build().RunAsync();
