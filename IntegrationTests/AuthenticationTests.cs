using Backend.AspPlugins;
using DatabaseMocker;
using IntegrationTests.Helpers;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTests
{
    
    public class AuthenticationTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<Program> _factory;
        

     
        public AuthenticationTests(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [Fact]
        public async Task Register_ReturnsCOnflict_WhenEmailAlreadyTaken()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AtriaContext>();

                // Seeding ...

                var user = context.Users.First();
                //Arrange
                Registration _registration = new()
                {
                    FirstNames = "Floppa",
                    LastName = "Floppington",
                    Password = "12345",
                    ConfirmPassword = "12345",
                    Email = user.Email,
                };

                //Act
                var response = await _client.PostAsJsonAsync("https://localhost:7038/api/auth/register", _registration);

                //Assert
                Assert.Equal(System.Net.HttpStatusCode.Conflict, response.StatusCode);
            }
        }
    }
}
