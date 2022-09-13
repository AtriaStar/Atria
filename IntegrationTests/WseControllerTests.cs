using IntegrationTests.BaseTestClasses;
using IntegrationTests.Helpers;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Net;
using System.Net.Http.Json;

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