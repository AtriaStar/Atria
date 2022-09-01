using Backend.AspPlugins;
using IntegrationTests.Helpers;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Models;
using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTests
{
    public class WseControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<Program> _factory;

        public WseControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [Fact]
        public async Task GetWse_ReturnsWse_WhenValidIdIsGiven()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                //Arrange
                var context = scope.ServiceProvider.GetRequiredService<AtriaContext>();
                var wse = context.WebserviceEntries.First();
                var wseId = wse.Id;

                //Act
                var response = await _client.GetAsync($"https://localhost:7038/api/wse/{wseId}");

                //Assert
                Assert.Equal(await response.Content.ReadFromJsonAsync<WebserviceEntry>(), wse);
            }
        }



    }
}
