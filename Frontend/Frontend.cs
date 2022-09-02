using Frontend;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using Microsoft.Extensions.Options;
using Models;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

Console.WriteLine("TEST");
Console.WriteLine(RootDirectory.GetTest());
Console.WriteLine(Directory.GetParent(RootDirectory.GetTest()));
Console.WriteLine(Directory.GetParent(RootDirectory.GetTest().Replace('\\', '/')));
// TODO: WTF????
builder.Configuration.AddStandardSources(builder.HostEnvironment.Environment);
var opt = builder.Configuration.CreateAtriaOptions<FrontendOptions>();

builder.Services
    .AddScoped<LoginState>()
    .AddScoped<CookieHandler>()
    .AddHttpClient(Options.DefaultName, client => client.BaseAddress = new($"{opt.AddressRoot}/{opt.ApiPrefix}/"))
    .AddHttpMessageHandler<CookieHandler>();

await builder.Build().RunAsync();
