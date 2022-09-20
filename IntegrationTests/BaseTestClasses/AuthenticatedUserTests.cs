using Backend.AspPlugins;
using IntegrationTests.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Models;

namespace IntegrationTests.BaseTestClasses;

public abstract class AuthenticatedUserTests : BaseTests, IAsyncLifetime {
    protected readonly IServiceScope Scope;
    protected readonly AtriaContext Context;
    protected Session Session = null!;
    protected User AuthenticatedUser = null!;

    protected AuthenticatedUserTests(CustomWebApplicationFactory<Program> factory) : base(factory) {
        Client.DefaultRequestHeaders.Add("Cookie", "Authorization=12345");
        Scope = Factory.Services.CreateScope();
        Context = Scope.ServiceProvider.GetRequiredService<AtriaContext>();
    }

    public async Task InitializeAsync() {
        Session = await Utilities.GetAuthenticatedUser(Context);
        AuthenticatedUser = Session.User;
    }
    
    public Task DisposeAsync() {
        Scope.Dispose();
        return Task.CompletedTask;
    }

    protected async Task<WebserviceEntry> AddBasicWseAsync()
        => (await Context.AddAsync(new WebserviceEntry {
            Name = Guid.NewGuid().ToString(),
            ShortDescription = "test",
            FullDescription = "test",
            Link = "https://www.test.com/" + Guid.NewGuid(),
            ViewCount = 1,
            ContactPersonId = AuthenticatedUser.Id,
            Collaborators = new List<Collaborator> { new() { User = AuthenticatedUser, Rights = WseRights.Owner } },
        })).Entity;
}
