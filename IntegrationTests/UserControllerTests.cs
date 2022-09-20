using IntegrationTests.BaseTestClasses;
using IntegrationTests.Helpers;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Net;
using System.Net.Http.Json;
using Backend;
using Backend.AspPlugins;
using Backend.ParameterHelpers;
using Microsoft.Extensions.DependencyInjection;

namespace IntegrationTests;

public class UserControllerTests : AuthenticatedUserTests {
    public UserControllerTests(CustomWebApplicationFactory<Program> factory) : base(factory) { }

    [Fact]
    public async Task GetAuthenticatedUser_ReturnsAuthenticatedUser_IfAuthenticatedUserExists() {
        //Act
        var responseAuthenticatedUser = await Client.GetFromJsonAsync<User>(
            $"https://localhost:7038/api/user/{AuthenticatedUser.Id}");

        //Assert
        Assert.NotNull(responseAuthenticatedUser);
        Assert.Equal(responseAuthenticatedUser.Id, AuthenticatedUser.Id);
    }

    [Fact]
    public async Task EditAuthenticatedUser_UpdatesUserInDb_IfAuthenticatedUserAuthorized() {
        //Arrange
        var newAuthenticatedUserName = "EditedName";

        //Act
        AuthenticatedUser.FirstNames = newAuthenticatedUserName;
        using var response = await Client.PostAsJsonAsync("https://localhost:7038/api/user", AuthenticatedUser);

        //Assert
        await response.AssertStatusCode();
        var userInDb = await Context.Users.FindAsync(AuthenticatedUser.Id);
        Assert.NotNull(userInDb);
        Assert.Equal(userInDb.FirstNames, newAuthenticatedUserName);
    }

    [Fact]
    public async Task DeleteAuthenticatedUser_DeletesUserInDb() {
        //Act
        using var response = await Client.DeleteAsync("https://localhost:7038/api/user");

        //Assert
        await response.AssertStatusCode();
        var userInDb = await Context.Users.FirstOrDefaultAsync(x => x.Id == AuthenticatedUser.Id);
        Assert.Null(userInDb);
    }

    [Fact]
    public async Task GetWseByAuthenticatedUser_ReturnsCorrectList() {
        //Arrange
        var newWse = await AddBasicWseAsync();
        await Context.SaveChangesAsync();

        //Act
        var wseList = await Client.GetFromJsonAsync<WebserviceEntry[]>($"https://localhost:7038/api/user/{AuthenticatedUser.Id}/wse");

        //Assert
        Assert.NotNull(wseList);
        Assert.NotEmpty(wseList);
        Assert.NotNull(wseList.FirstOrDefault(x => x.Id == newWse.Id));
    }

    [Fact]
    public async Task GetBookmarksByAuthenticatedUser_ReturnsCorrectList() {
        //Arrange
        var wse = await AddBasicWseAsync();
        AuthenticatedUser.Bookmarks.Add(wse);
        await Context.SaveChangesAsync();

        //Act
        var bookmarkList = await Client.GetFromJsonAsync<WebserviceEntry[]>(
            $"https://localhost:7038/api/user/{AuthenticatedUser.Id}/bookmarks");

        //Assert
        Assert.NotNull(bookmarkList);
        Assert.NotEmpty(bookmarkList);
    }

    [Fact]
    public async Task GetReviewsByAuthenticatedUser_ReturnsCorrectList() {
        //Arrange
        var wse = await AddBasicWseAsync();
        var review = new Review {
            Title = "testReview",
            StarCount = StarCount.Five,
            Creator = AuthenticatedUser,
            CreatorId = AuthenticatedUser.Id,
            Wse = wse,
            WseId = wse.Id,
            Description = "testDescription",
        };
        await Context.Reviews.AddAsync(review);
        await Context.SaveChangesAsync();

        //Act
        var reviewList = await Client.GetFromJsonAsync<Review[]>(
            $"https://localhost:7038/api/user/{AuthenticatedUser.Id}/reviews");

        //Assert
        Assert.NotNull(reviewList);
        Assert.NotEmpty(reviewList);
    }

    [Fact]
    public async Task AddBookmark_UpdatesUserInDb() {
        //Arrange
        var wse = await AddBasicWseAsync();
        await Context.SaveChangesAsync();

        //Act
        using var response = await Client.PostAsync($"https://localhost:7038/api/user/{AuthenticatedUser.Id}/bookmarks/add/{wse.Id}", null);
        var userInDb = await Context.Users.Include(x => x.Bookmarks).FirstOrDefaultAsync(x => x.Id == AuthenticatedUser.Id);

        //Assert
        await response.AssertStatusCode();
        Assert.NotNull(userInDb);
        Assert.Contains(wse, userInDb.Bookmarks);
    }

    [Fact]
    public async Task RemoveBookmark_UpdatesUserInDb() {
        //Arrange
        var wse = await AddBasicWseAsync();
        AuthenticatedUser.Bookmarks.Add(wse);
        await Context.SaveChangesAsync();

        //Act
        using var response = await Client.PostAsync($"https://localhost:7038/api/user/{AuthenticatedUser.Id}/bookmarks/remove/{wse.Id}", null);

        // TODO: I have no idea why the fuck this is necessary. Entry.Reload should totally do the job, but it just doesn't.
        using var scope = Factory.Services.CreateScope();
        var ctx = scope.ServiceProvider.GetRequiredService<AtriaContext>();
        var user = await ctx.Users.FindAsync(AuthenticatedUser.Id);

        //Assert
        await response.AssertStatusCode();
        Assert.NotNull(user);
        Assert.DoesNotContain(wse.Id, user.Bookmarks.Select(x => x.Id));
    }
}
