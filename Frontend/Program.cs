using Frontend;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new(builder.Configuration["BaseAddress"] ?? throw new InvalidOperationException("no backend address provided")) });
builder.Services.AddScoped<LoginManager>();
await builder.Build().RunAsync();
