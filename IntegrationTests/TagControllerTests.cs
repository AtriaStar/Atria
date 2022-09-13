using IntegrationTests.BaseTestClasses;
using IntegrationTests.Helpers;
using Models;
using System.Net;
using System.Net.Http.Json;
using System.Text;

namespace IntegrationTests {
    public class TagControllerTests : AuthenticatedUserTests {

        public TagControllerTests(CustomWebApplicationFactory<Program> factory) : base(factory) { }

        [Fact]
        public async Task GetAll_ReturnsAllTags() {

            //Arrange
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

            await Context.Tags.AddAsync(tag1);
            await Context.Tags.AddAsync(tag2);
            await Context.Tags.AddAsync(tag3);

            //Act
            HttpRequestMessage request = new(HttpMethod.Get, $"https://localhost:7038/api/tag");

            var response = await Client.SendAsync(request);
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

        [Fact]
        public async Task CreateTag_ReturnsOkResult_WhenUserAuthenticated() {

            //Arrange
            var session = await Utilities.GetAuthenticatedUser(Context);
            var authenticatedUser = session.User;
            var tagName = "CreateTagTest";
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(tagName);

            //Act
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, $"https://localhost:7038/api/tag");
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");
            request.Headers.Add("Cookie", "Authorization=12345");

            var response = await Client.SendAsync(request);
            var tagInDb = Context.Tags.FirstOrDefault(x => x.Name.Equals(tagName));

            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(tagInDb);
        }

        [Fact]
        public async Task Merge_ReturnsOkResult_WhenUserAuthorized() {

            //Arrange
            var session = await Utilities.GetAuthenticatedUser(Context);
            var authenticatedUser = session.User;
            var newTagName = "newMergeTag";

            //Todo: method for creating random tags
            var tag = new Tag() {
                Name = "MergeTag",
                Description = "GetMergeDescription",
            };
            await Context.Tags.AddAsync(tag);

            //var json = Newtonsoft.Json.JsonConvert.SerializeObject(tagName);

            //Act


            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"https://localhost:7038/api/tag/merge/{newTagName}/{tag.Name}");
            //request.Content = new StringContent(json, Encoding.UTF8, "application/json");
            request.Headers.Add("Cookie", "Authorization=12345");

            var response = await Client.SendAsync(request);
            var tagInDb = Context.Tags.FirstOrDefault(x => x.Name.Equals(newTagName));

            //Assert
            if (tagInDb == null) {
                Assert.True(false, "tag not in db");
                return;
            }
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);


        }
    }
}

