using IntegrationTests.BaseTestClasses;
using IntegrationTests.Helpers;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.DTO;
using System.Net;
using System.Net.Http.Json;
using System.Runtime.Intrinsics.X86;

namespace IntegrationTests; 

public class WseControllerTests : AuthenticatedUserTests {
    public WseControllerTests(CustomWebApplicationFactory<Program> factory) : base(factory) { }

    [Fact]
    public async Task GetWse_ReturnsWse_WhenValidIdIsGiven() {
        //Arrange
        var wse = Context.WebserviceEntries.First();
        var wseId = wse.Id;

        //Act
        var responseWse = await Client.GetFromJsonAsync<WebserviceEntry>($"https://localhost:7038/api/wse/{wseId}");

        //Assert
        Assert.NotNull(responseWse);
        Assert.Equal(responseWse.Id, wse.Id);
    }

    [Fact]
    public async Task CreateWse_AddsWseToDb_WhenUserAuthenticated() {
        //Arrange
        var newWse = new WebserviceEntry {
            Name = "CreatedWse",
            ShortDescription = "test",
            FullDescription = "test",
            Link = "https://www.test.com/",
            ViewCount = 1,
            ContactPersonId = AuthenticatedUser.Id,
        };
        
        //Act
        using var response = await Client.PutAsJsonAsync("https://localhost:7038/api/wse", newWse);

        //Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task CreateQuestion_AddsQuestionToDb_WhenUserAuthenticated() {
        //Arrange
        var wse = await Context.WebserviceEntries.FirstAsync();
        var newQuestion = new Question {
            Creator = AuthenticatedUser,
            CreatorId = AuthenticatedUser.Id,
            Wse = wse,
            WseId = wse.Id,
            CreationTime = DateTimeOffset.UtcNow,
            Text = "test",
        };

        //Act
        using var response = await Client.PutAsJsonAsync("https://localhost:7038/api/wse/question", newQuestion);

        //Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task CreateAnswer_AddsQuestionToDb_WhenUserAuthenticated() {
        //Arrange
        var wse = await Context.WebserviceEntries.FirstAsync();
        var newQuestion = new Question {
            Creator = AuthenticatedUser,
            CreatorId = AuthenticatedUser.Id,
            Wse = wse,
            WseId = wse.Id,
            CreationTime = DateTimeOffset.UtcNow,
            Text = "test",
        };
     
        
        //Act
        await Client.PutAsJsonAsync("https://localhost:7038/api/wse/question", newQuestion);
        var questionInDb = Context.Questions.First();
        var newAnswer = new Answer {
            Text = "test",
            Wse = wse,
            WseId = wse.Id,
            CreationTime = DateTimeOffset.UtcNow,
            Creator = AuthenticatedUser,
            CreatorId = AuthenticatedUser.Id,
            Question = questionInDb,
            QuestionId = questionInDb.Id,
        };
        using var response = await Client.PutAsJsonAsync("https://localhost:7038/api/wse/answer", newAnswer);

        //Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task CreateReview_AddsWseToDb_WhenUserAuthenticated() {
        //Arrange
        var wse = await Context.WebserviceEntries.FirstAsync();
        var newReview = new Review {
            Title = "testReview",
            StarCount = StarCount.Five,
            Creator = AuthenticatedUser,
            CreatorId = AuthenticatedUser.Id,
            Wse = wse,
            WseId = wse.Id,
            Description = "testDescription",
        };

        //Act
        using var response = await Client.PutAsJsonAsync("https://localhost:7038/api/wse/review", newReview);

        //Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }


    [Fact]
    public async Task EditWse_UpdatesWseInDb_WhenUserAuthorized() {
        //Arrange
        var newWse = new WebserviceEntry {
            Name = "EditWseAuhtorized",
            ShortDescription = "Search things",
            FullDescription = "search many things",
            Link = "https://www.Edit.com/",
            ViewCount = 1,
            ContactPersonId = AuthenticatedUser.Id,
        };
        newWse.Collaborators.Add(new() { UserId = AuthenticatedUser.Id, Rights = WseRights.Owner });
        newWse = (await Context.WebserviceEntries.AddAsync(newWse)).Entity;
        await Context.SaveChangesAsync();

        //Act
        var realizedWse = await Client.GetFromJsonAsync<WebserviceEntry>($"https://localhost:7038/api/wse/{newWse.Id}");
        realizedWse!.Name = "HackedByBigFlopa";
        using var response = await Client.PostAsJsonAsync("https://localhost:7038/api/wse", realizedWse);
        await response.AssertStatusCode();
        var wseInDb = await Context.WebserviceEntries.FirstOrDefaultAsync(x => x.Name.Equals(realizedWse.Name));

        //Assert
        Assert.NotNull(wseInDb);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(wseInDb.Name, newWse.Name);
    }

    [Fact]
    public async Task EdiReview_UpdatesReviewInDb_WhenUserAuthorized() {
        //Arrange
        var wse = await Context.WebserviceEntries.FirstAsync();
        var review = new Review {
            Title = "testReview",
            StarCount = StarCount.Five,
            Creator = AuthenticatedUser,
            CreatorId = AuthenticatedUser.Id,
            Wse = wse,
            WseId = wse.Id,
            Description = "testDescription",
        };
        await Context.Reviews.AddRangeAsync(review);
        await Context.SaveChangesAsync();
        //Act
        var realizedReview = await Context.Reviews.FirstOrDefaultAsync(x => x.Title.Equals(review.Title));
        realizedReview!.Title = "HackedByBigFlopa";
        using var response = await Client.PostAsJsonAsync("https://localhost:7038/api/wse/review", realizedReview);
        await response.AssertStatusCode();
        var reviewInDb = await Context.Reviews.FirstOrDefaultAsync(x => x.Title.Equals(realizedReview.Title));

        //Assert
        Assert.NotNull(reviewInDb);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
     
    }

    [Fact]
    public async Task EditCollaborators_UpdatesWseInDb_WhenUserAuthorized() {
        //Arrange
        var newWse = new WebserviceEntry {
            Name = "EditCollabs",
            ShortDescription = "Search things",
            FullDescription = "search many things",
            Link = "https://www.EditCollab.com/",
            ViewCount = 1,
            ContactPersonId = AuthenticatedUser.Id,
        };
        newWse.Collaborators.Add(new() { UserId = AuthenticatedUser.Id, Rights = WseRights.Owner });
        newWse = (await Context.WebserviceEntries.AddAsync(newWse)).Entity;
        await Context.SaveChangesAsync();
        var user = await Context.Users.FirstOrDefaultAsync(x => x.Id != AuthenticatedUser.Id);

        //Act
        var collabDto = new CollaboratorDto {
            UserId = user.Id,
            Rights = WseRights.EditCollaborators,
        };
        CollaboratorDto[] collabs = {collabDto};

        using var response = await Client.PostAsJsonAsync($"https://localhost:7038/api/wse/{newWse.Id}/collaborators", collabs);
        await response.AssertStatusCode();
        var wseInDb = await Context.WebserviceEntries.FirstOrDefaultAsync(x => x.Name.Equals(newWse.Name));
        var userInCollabList = wseInDb.Collaborators.FirstOrDefault(x => x.UserId == user.Id);

        //Assert
        Assert.NotNull(wseInDb);
        Assert.NotNull(userInCollabList);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task EditWse_ReturnsUnauthorized_WhenUserAuthenticatedButUnauthorized() {
        //Arrange
        var wse = await Context.WebserviceEntries
            .Include(x => x.Collaborators)
            .FirstAsync(x => x.Collaborators.All(y => y.UserId != AuthenticatedUser.Id));

        wse.Name = "EditedWseUnauthorized";

        //Act
        using var response = await Client.PostAsJsonAsync("https://localhost:7038/api/wse", wse);
        var test = await response.Content.ReadAsStringAsync();

        //Assert
        Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
    }

    [Fact]
    public async Task GetAfterCreate_GetsCorrectWse_WhenUserAuthenticated() {
        //Arrange
        var newWse = new WebserviceEntry {
            Name = "CreatedWse",
            ShortDescription = "test",
            FullDescription = "test",
            Link = "https://www.test.com/",
            ViewCount = 1,
            ContactPersonId = AuthenticatedUser.Id,
        };
        
        //Act
        using var responseCreate = await Client.PutAsJsonAsync("https://localhost:7038/api/wse", newWse);
        await responseCreate.AssertStatusCode();
        var wseInDb = await Context.WebserviceEntries.FirstOrDefaultAsync(x => x.Name.Equals(newWse.Name));

        Assert.NotNull(wseInDb);
        
        var responseGetWse = await Client.GetFromJsonAsync<WebserviceEntry>($"https://localhost:7038/api/wse/{wseInDb.Id}");
        
        //Assert
        Assert.NotNull(responseGetWse);
        Assert.Equal(responseGetWse.Id, wseInDb.Id);
    }
}