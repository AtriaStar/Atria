using System.Net;
using Bunit;
using Frontend;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Models;
using RichardSzalay.MockHttp;

namespace bUnitTests;

public static class MockAuthentication {
    public static async Task AddMockAuthentication(TestContext context,
        MockHttpMessageHandler mockHttpMessageHandler, bool isAuthorized) {
        if (context is null) throw new ArgumentNullException(nameof(context));

        if (isAuthorized) {
            var mockUser = new User {
                Id = 1,
                FirstNames = "MockFirstName",
                LastName = "MockLastName",
                Email = "mock@mock",
                SignUpIp = "127.0.0.1",
                Rights = UserRights.Default,
                CreatedAt = DateTimeOffset.UtcNow
            };

            mockHttpMessageHandler.When("http://localhost/auth").RespondJson(mockUser);
        } else {
            mockHttpMessageHandler.When("http://localhost/auth").Respond(HttpStatusCode.Unauthorized);
        }
        LoginState l = new LoginState(context.Services.GetRequiredService<HttpClient>());
        await l.Init;

        context.RenderTree.Add<CascadingValue<LoginState>>(parameters => parameters.Add(
            p => p.Value, l));
            
    }
}
