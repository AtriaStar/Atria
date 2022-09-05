using Backend.AspPlugins;
using IntegrationTests.Helpers;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTests
{
    public class TagControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<Program> _factory;

        public TagControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [Fact]
        public async Task CreateTag_ReturnsOkResult_WhenUserAuthenticated()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                //Arrange
                var context = scope.ServiceProvider.GetRequiredService<AtriaContext>();
                AuthenticationAuthorizationUserFactory userFactory = new AuthenticationAuthorizationUserFactory(context);
                var facoryResult = await userFactory.GetAuthenticatedUser();
                var authenticatedUser = facoryResult.Item1;
                var session = facoryResult.Item2;

                var tagName = "CreateTagTest";

                var json = Newtonsoft.Json.JsonConvert.SerializeObject(tagName);

                //Act
                using (var handler = new HttpClientHandler { UseCookies = false })
                {

                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, $"https://localhost:7038/api/tag");
                    request.Content = new StringContent(json, Encoding.UTF8, "application/json");
                    request.Headers.Add("Cookie", "Authorization=12345");

                    var response = await _client.SendAsync(request);
                    var tagInDb = context.Tags.FirstOrDefault(x => x.Name.Equals(tagName));
                    
                    //Assert

                    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                    Assert.NotNull(tagInDb);
                }
            }
        }
    }
}
