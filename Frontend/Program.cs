using Frontend;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using Microsoft.Extensions.Options;
using Models;
using System.Net.Http.Json;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services
    .AddScoped(async provider => {
        var cnt = await provider.GetRequiredService<HttpClient>().GetAsync("auth");
        if (!cnt.IsSuccessStatusCode) { return null; }
        return await cnt.Content.ReadFromJsonAsync<User>();
    })
    .AddScoped<CookieHandler>()
    .AddHttpClient(Options.DefaultName, client => client.BaseAddress =
        new(builder.Configuration["BaseAddress"] ?? throw new InvalidOperationException("No backend address provided")))
    .AddHttpMessageHandler<CookieHandler>();

await builder.Build().RunAsync();
