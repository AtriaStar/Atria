using IntegrationTests.Helpers;
using Microsoft.AspNetCore.Mvc.Testing;
using Models.DTO;
using System.Net.Http.Json;

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

        [Fact]
        public async Task GetWse_ReturnsNotFound_WhenRequestInvalid()
        {
            //Arrange
            Registration _registration = new()
            {
                FirstNames = "Floppa",
                LastName = "Floppington",
                Password = "12345",
                ConfirmPassword = "12345",
                Email = "floppa12@email.com"
            };
            

            //Act
            var response = await _client.PostAsJsonAsync("https://localhost:7038/api/auth/register", _registration);

            //Assert
            Console.Out.WriteLine(response.StatusCode);
            Console.Out.WriteLine(response.Content.ToString());
            Assert.True(response.IsSuccessStatusCode);
        }
    }
            
}
