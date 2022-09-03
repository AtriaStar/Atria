using Backend.AspPlugins;
using IntegrationTests.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Models;
using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xunit.Sdk;

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
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"https://localhost:7038/api/wse/{wseId}");
                var response = await _client.SendAsync(request);
                var responseWse = await response.Content.ReadFromJsonAsync<WebserviceEntry>();

                //Assert
                if(responseWse == null)
                {
                    Assert.True(false, "response is null");
                    return;
                }
                
                Assert.Equal(responseWse.Id , wse.Id);
            }
        }

        [Fact]
        public async Task CreateWse_AddsWseToDb_WhenUserAuthenticated()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                //Arrange
                var context = scope.ServiceProvider.GetRequiredService<AtriaContext>();
                var wse = context.WebserviceEntries.First();
                var wseId = wse.Id;
                AuthenticationAuthorizationUserFactory userFactory = new AuthenticationAuthorizationUserFactory(context);
                var facoryResult = await userFactory.GetAuthenticatedUser();
                var authenticatedUser = facoryResult.Item1;
                var session = facoryResult.Item2;

                WebserviceEntry newWse = new WebserviceEntry
                {
                    Name = "Bing",
                    ShortDescription = "Search things",
                    FullDescription = "search many things",
                    Link = "https://www.bing.com/",
                    ViewCount = 1,
                    ContactPersonId = authenticatedUser.Id,
                    
                    
                };
        
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(newWse);

                //Act
                using (var handler = new HttpClientHandler { UseCookies = false })
                {

                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, $"https://localhost:7038/api/wse");
                    request.Content = new StringContent(json, Encoding.UTF8, "application/json");
                    request.Headers.Add("Cookie", "Authorization=12345");
                   
                    var response = await _client.SendAsync(request);


                    //Assert

                    Assert.Equal(HttpStatusCode.Created, response.StatusCode);
                }
            }
        }
    }
}
