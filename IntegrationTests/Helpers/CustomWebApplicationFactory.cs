using Backend.AspPlugins;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Net;

namespace IntegrationTests.Helpers; 

public class CustomWebApplicationFactory<TStartup>
    : WebApplicationFactory<TStartup> where TStartup : class {
    protected override void ConfigureWebHost(IWebHostBuilder builder) {
        builder.ConfigureServices(services => {
            services.AddSingleton<IStartupFilter, CustomStartupFilter>();

            var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<AtriaContext>();
            var logger = scopedServices
                .GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

            db.Database.EnsureCreated();

            try {
                Utilities.InitializeDbForTests(db);
            } catch (Exception ex) {
                logger.LogError(ex, "An error occurred seeding the " +
                                    "database with test messages. Error: {Message}", ex.Message);
            }
        });
    }
}

public class CustomStartupFilter : IStartupFilter {
    public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next) {
        return app => {
            app.UseMiddleware<FakeRemoteIpAddressMiddleware>();
            next(app);
        };
    }
}

public class FakeRemoteIpAddressMiddleware {
    private readonly RequestDelegate next;
    private readonly IPAddress fakeIpAddress = IPAddress.Parse("127.0.0.1");

    public FakeRemoteIpAddressMiddleware(RequestDelegate next) {
        this.next = next;
    }

    public async Task Invoke(HttpContext httpContext) {
        httpContext.Connection.RemoteIpAddress = fakeIpAddress;
        //Todo: not like this
        httpContext.Request.Headers["User-Agent"] = "";


        await next(httpContext);
    }
}