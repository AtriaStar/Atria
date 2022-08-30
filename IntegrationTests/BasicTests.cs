using IntegrationTests.Helpers;
using Microsoft.AspNetCore.Mvc.Testing;

namespace IntegrationTests
{
    public class BasicTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<Program> _factory;

        public BasicTests(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }  
    }
}
