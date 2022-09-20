using IntegrationTests.BaseTestClasses;
using IntegrationTests.Helpers;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Net;
using System.Net.Http.Json;

namespace IntegrationTests;

public class UserControllerTests : AuthenticatedUserTests {
    public UserControllerTests(CustomWebApplicationFactory<Program> factory) : base(factory) { }

    [Fact]
    public async Task GetUser_ReturnsUser_IfUserExists() {
        //Arrange
        var user = await Context.Users.FirstAsync();
        var userId = user.Id;
        //Act
        var responseUser = await Client.GetFromJsonAsync<User>($"https://localhost:7038/api/user/{userId}");

        //Assert
        Assert.NotNull(responseUser);
        Assert.Equal(responseUser.Id, userId);
    }

    [Fact]
    public async Task EditUser_UpdatesUserInDb_IfUserAuthorized() {
        //Arrange
        var user = AuthenticatedUser;
        var newUserName = "EditedName";
        //Act
        user!.FirstNames = newUserName;
        var response = await Client.PostAsJsonAsync("https://localhost:7038/api/user", user);
        //Assert
        var userInDb = await Context.Users.FirstOrDefaultAsync(x => x.Id == AuthenticatedUser.Id);
        Assert.NotNull(userInDb);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(userInDb.FirstNames, newUserName);
    }

    [Fact]
    public async Task DeleteUser_DeletesUserInDb() {
        //Arrange
        var user = AuthenticatedUser;
        //Act
        var response = await Client.DeleteAsync("https://localhost:7038/api/user");
        //Assert

        var userInDb = await Context.Users.FirstOrDefaultAsync(x => x.Id == user.Id);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Null(userInDb);
    }

    [Fact]
    public async Task GetWseByUser_ReturnsCorrectList() {
        //Arrange
        var user = AuthenticatedUser;
        var wseName = "GetWseByUser";
        var newWse = new WebserviceEntry {
            Name = wseName,
            ShortDescription = "test",
            FullDescription = "test",
            Link = "https://www.test.com/",
            ViewCount = 1,
            ContactPersonId = user.Id,
            Collaborators = new List<Collaborator> { new() { User = user, Rights = WseRights.Owner } }
        };
        await Context.WebserviceEntries.AddAsync(newWse);
        await Context.SaveChangesAsync();
        //Act
        var response = await Client.GetAsync($"https://localhost:7038/api/user/{user.Id}/wse");
        var wseList = await response.Content.ReadFromJsonAsync<IEnumerable<WebserviceEntry>>() ?? Array.Empty<WebserviceEntry>();
        //Assert

        Assert.NotEmpty(wseList);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(wseList.FirstOrDefault(x => x.Name.Equals(newWse.Name)));
    }

    [Fact]
    public async Task GetBookmarksByUser_ReturnsCorrectList() {
        //Arrange
        var user = AuthenticatedUser;
        var wse = await Context.WebserviceEntries.FirstAsync();
        user.Bookmarks.Add(wse);
        Context.Users.Update(user);

        //Act
        var response = await Client.GetAsync($"https://localhost:7038/api/user/{user.Id}/bookmarks");
        var bookmarkList = await response.Content.ReadFromJsonAsync<IEnumerable<WebserviceEntry>>() ?? Array.Empty<WebserviceEntry>();
        //Assert

        Assert.NotEmpty(bookmarkList);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetReviewsByUser_ReturnsCorrectList() {
        //Arrange
        var user = await Context.Users.FirstAsync();
        var wse = await Context.WebserviceEntries.FirstAsync();
        var review = new Review {
            Title = "testReview",
            StarCount = StarCount.Five,
            Wse = wse,
            Creator = user,
            CreatorId = user.Id,
            WseId = wse.Id,
            Description = "testDescription",
            CreationTime = DateTimeOffset.UtcNow,
        };
        await Context.Reviews.AddAsync(review);
        await Context.SaveChangesAsync();

        //Act
        var response = await Client.GetAsync($"https://localhost:7038/api/user/{user.Id}/reviews");
        var reviewList = await response.Content.ReadFromJsonAsync<IEnumerable<Review>>() ?? Array.Empty<Review>();
        //Assert

        Assert.NotEmpty(reviewList);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetWseDraftsByUser_ReturnsCorrectList() {
        //Arrange
        var user = AuthenticatedUser;
        var wseDraft = new WseDraft { Name = "testDraft" };
        user.WseDrafts.Add(wseDraft);
        Context.Users.Update(user);
        await Context.SaveChangesAsync();

        //Act
        var response = await Client.GetAsync($"https://localhost:7038/api/user/{user.Id}/drafts");
        var wseDraftList = await response.Content.ReadFromJsonAsync<IEnumerable<WseDraft>>() ?? Array.Empty<WseDraft>();
        //Assert

        Assert.NotEmpty(wseDraftList);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task AddBookmark_UpdatesUserInDb() {
        //Arrange
        var user = await Context.Users.FirstAsync();
        var wse = await Context.WebserviceEntries.FirstAsync();
        //Act
        HttpRequestMessage request = new(HttpMethod.Post, $"https://localhost:7038/api/user/{user.Id}/bookmarks/add/{wse.Id}");
        var response = await Client.SendAsync(request);
        var userInDb = await Context.Users.FirstOrDefaultAsync(x => x.FirstNames == user.FirstNames);

        //Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(userInDb);
        Assert.Contains(wse, userInDb.Bookmarks);
    }

    [Fact]
    public async Task RemoveBookmark_UpdatesUserInDb() {
        //Arrange
        var user = await Context.Users.FirstAsync();
        var wse = await Context.WebserviceEntries.FirstAsync();
        user.Bookmarks.Add(wse);
        Context.Users.Update(user);
        Context.SaveChanges();

        //Act
        HttpRequestMessage request = new(HttpMethod.Post, $"https://localhost:7038/api/user/{user.Id}/bookmarks/delete/{wse.Id}");
        var response = await Client.SendAsync(request);
        var userInDb = await Context.Users.FirstOrDefaultAsync(x => x.FirstNames == user.FirstNames);

        //Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(userInDb);
        Assert.DoesNotContain(wse, userInDb.Bookmarks);

    }
}
