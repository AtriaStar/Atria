using IntegrationTests.Helpers;

namespace IntegrationTests.BaseTestClasses; 

public abstract class BaseTests : IClassFixture<CustomWebApplicationFactory<Program>> {
    protected readonly HttpClient Client;
    protected readonly CustomWebApplicationFactory<Program> Factory;

    protected BaseTests(CustomWebApplicationFactory<Program> factory) {
        Factory = factory;
        Client = factory.CreateClient();
    }
}
