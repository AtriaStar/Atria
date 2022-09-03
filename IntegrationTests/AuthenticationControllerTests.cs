using Backend.AspPlugins;
using DatabaseMocker;
using IntegrationTests.Helpers;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
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
    
    public class AuthenticationControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<Program> _factory;
        

     
        public AuthenticationControllerTests(CustomWebApplicationFactory<Program> factory)
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

                var user = await context.Users.FirstAsync();
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

        [Fact]
        public async Task Register_ReturnsSessionCookie_WhenRegistrationValid()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                //Arrange
                var context = scope.ServiceProvider.GetRequiredService<AtriaContext>();
                var user = await context.Users.FirstAsync();
                
                Registration _registration = new()
                {
                    FirstNames = "Floppa",
                    LastName = "Floppington",
                    Password = "12345",
                    ConfirmPassword = "12345",
                    Email = "validEmail@email.com",
                };

                //Act
                var response = await _client.PostAsJsonAsync("https://localhost:7038/api/auth/register", _registration);
                
                //Assert
                
                //Todo: check if authentication cookie is set in response and database
            }
        }

        [Fact]
        public async Task Login_ReturnsUnauthorized_WhenLoginInvalid()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                //Arrange
                var context = scope.ServiceProvider.GetRequiredService<AtriaContext>();
                var user = await context.Users.FirstAsync();

                Login login1 = new()
                {
                    Password = "12345",
                    Email = "inValidEmail@email.com",
                };

                Login login2 = new()
                {
                    Password = "12345",
                    Email = user.Email,
                };

                //Act
                var response1 = await _client.PostAsJsonAsync("https://localhost:7038/api/auth/login", login1);
                var response2 = await _client.PostAsJsonAsync("https://localhost:7038/api/auth/login", login2);
                //Assert

                Assert.Equal(System.Net.HttpStatusCode.Unauthorized, response1.StatusCode);
                Assert.Equal(System.Net.HttpStatusCode.Unauthorized, response2.StatusCode);
            }
        }

        [Fact]
        public async Task RegisterLogin_ReturnsSessionCookie_WhenLoginValid()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                //Arrange
                var context = scope.ServiceProvider.GetRequiredService<AtriaContext>();

                Registration _registration = new()
                {
                    FirstNames = "Floppa",
                    LastName = "Floppington",
                    Password = "12345",
                    ConfirmPassword = "12345",
                    Email = "testEmail@email.com",
                };

                Login login = new()
                {
                    Password = "12345",
                    Email = "testEmail@email.com",
                };

                //Act
                await _client.PostAsJsonAsync("https://localhost:7038/api/auth/register", _registration);
                var response = await _client.PostAsJsonAsync("https://localhost:7038/api/auth/login", login);

                //Assert

                Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
                //Todo: check if authentication cookie is set in response and database
            }
        }
    }
}
