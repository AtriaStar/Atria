using Backend.AspPlugins;
using IntegrationTests.Helpers;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Models;
using System.Net;
using System.Net.Http.Json;
using System.Text;

namespace IntegrationTests {
    public class TagControllerTests : IClassFixture<CustomWebApplicationFactory<Program>> {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory<Program> _factory;

        public TagControllerTests(CustomWebApplicationFactory<Program> factory) {
            _factory = factory;
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions {
                AllowAutoRedirect = false
            });
        }
        [Fact]
        public async Task GetAll_ReturnsAllTags() {
            using (var scope = _factory.Services.CreateScope()) {
                //Arrange
                var context = scope.ServiceProvider.GetRequiredService<AtriaContext>();

                //Todo: method for creating random tags
                var tag1 = new Tag() {
                    Name = "GetAllTag1",
                    Description = "GetAllTag1Description",
                    CreationTime = DateTimeOffset.UtcNow,
                };

                var tag2 = new Tag() {
                    Name = "GetAllTag2",
                    Description = "GetAllTag2Description",
                    CreationTime = DateTimeOffset.UtcNow,
                };

                var tag3 = new Tag() {
                    Name = "GetAllTag3",
                    Description = "GetAllTag3Description",
                    CreationTime = DateTimeOffset.UtcNow,
                };

                await context.Tags.AddAsync(tag1);
                await context.Tags.AddAsync(tag2);
                await context.Tags.AddAsync(tag3);

                //Act
                HttpRequestMessage request = new(HttpMethod.Get, $"https://localhost:7038/api/tag");

                var response = await _client.SendAsync(request);
                var responseContent = await response.Content.ReadFromJsonAsync<IQueryable<Tag>>();

                //Assert
                if (responseContent == null) {
                    Assert.True(false, "tags not added to db");
                    return;
                }
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                Assert.Equal(3, responseContent.Count());
                Assert.True(responseContent.Contains(tag1));
                Assert.True(responseContent.Contains(tag2));
                Assert.True(responseContent.Contains(tag3));
            }
        }

        [Fact]
        public async Task CreateTag_ReturnsOkResult_WhenUserAuthenticated() {
            using (var scope = _factory.Services.CreateScope()) {
                //Arrange
                var context = scope.ServiceProvider.GetRequiredService<AtriaContext>();
                var session = await Utilities.GetAuthenticatedUser(context);
                var authenticatedUser = session.User;

                var tagName = "CreateTagTest";

                var json = Newtonsoft.Json.JsonConvert.SerializeObject(tagName);

                //Act
                using (var handler = new HttpClientHandler { UseCookies = false }) {

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

        [Fact]
        public async Task Merge_ReturnsOkResult_WhenUserAuthorized() {
            using (var scope = _factory.Services.CreateScope()) {
                //Arrange
                var context = scope.ServiceProvider.GetRequiredService<AtriaContext>();
                var session = await Utilities.GetAuthenticatedUser(context);
                var authenticatedUser = session.User;
                var newTagName = "newMergeTag";

                //Todo: method for creating random tags
                var tag = new Tag() {
                    Name = "MergeTag",
                    Description = "GetMergeDescription",
                };
                await context.Tags.AddAsync(tag);

                //var json = Newtonsoft.Json.JsonConvert.SerializeObject(tagName);

                //Act
                using (var handler = new HttpClientHandler { UseCookies = false }) {

                    HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"https://localhost:7038/api/tag/merge/{newTagName}/{tag.Name}");
                    //request.Content = new StringContent(json, Encoding.UTF8, "application/json");
                    request.Headers.Add("Cookie", "Authorization=12345");

                    var response = await _client.SendAsync(request);
                    var tagInDb = context.Tags.FirstOrDefault(x => x.Name.Equals(newTagName));

                    //Assert
                    if (tagInDb == null) {
                        Assert.True(false, "tag not in db");
                        return;
                    }
                    Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                }
            }
        }
    }
}
