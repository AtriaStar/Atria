using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IntegrationTests.BaseTestClasses;
using IntegrationTests.Helpers;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Net;
using System.Net.Http.Json;
using System.Runtime.Intrinsics.X86;

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
}
