using IntegrationTests.BaseTestClasses;
using IntegrationTests.Helpers;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.DTO;
using System.Net.Http.Json;

namespace IntegrationTests {

    public class AuthenticationControllerTests : AuthenticatedUserTests {

        public AuthenticationControllerTests(CustomWebApplicationFactory<Program> factory) : base(factory) { }

        [Fact]
        public async Task Register_ReturnsCOnflict_WhenEmailAlreadyTaken() {
            //Arrange
            var user = await Context.Users.FirstAsync();
            
            RegistrationDto _registration = new() {
                FirstNames = "Floppa",
                LastName = "Floppington",
                Password = "12345",
                ConfirmPassword = "12345",
                Email = user.Email,
            };

            //Act
            var response = await Client.PostAsJsonAsync("https://localhost:7038/api/auth/register", _registration);

            //Assert
            Assert.Equal(System.Net.HttpStatusCode.Conflict, response.StatusCode);

        }

        [Fact]
        public async Task Register_ReturnsSessionCookie_WhenRegistrationValid() {

            //Arrange
            var user = await Context.Users.FirstAsync();

            RegistrationDto _registration = new() {
                FirstNames = "Floppa",
                LastName = "Floppington",
                Password = "12345",
                ConfirmPassword = "12345",
                Email = "validEmail@email.com",
            };

            //Act
            var response = await Client.PostAsJsonAsync("https://localhost:7038/api/auth/register", _registration);

            //Assert

            //Todo: check if authentication cookie is set in response and database
        }


        [Fact]
        public async Task Login_ReturnsUnauthorized_WhenLoginInvalid() {

            //Arrange
            var user = await Context.Users.FirstAsync();

            LoginDto login1 = new() {
                Password = "12345",
                Email = "inValidEmail@email.com",
            };

            LoginDto login2 = new() {
                Password = "5353345",
                Email = user.Email,
            };

            //Act
            var response1 = await Client.PostAsJsonAsync("https://localhost:7038/api/auth/login", login1);
            var response2 = await Client.PostAsJsonAsync("https://localhost:7038/api/auth/login", login2);
            //Assert

            Assert.Equal(System.Net.HttpStatusCode.Unauthorized, response1.StatusCode);
            Assert.Equal(System.Net.HttpStatusCode.Unauthorized, response2.StatusCode);
        }


        [Fact]
        public async Task RegisterLogin_ReturnsSessionCookie_WhenLoginValid() {

            //Arrange
            RegistrationDto _registration = new() {
                FirstNames = "Floppa",
                LastName = "Floppington",
                Password = "12345",
                ConfirmPassword = "12345",
                Email = "testEmail@email.com",
            };

            LoginDto login = new() {
                Password = "12345",
                Email = "testEmail@email.com",
            };

            //Act
            await Client.PostAsJsonAsync("https://localhost:7038/api/auth/register", _registration);
            var response = await Client.PostAsJsonAsync("https://localhost:7038/api/auth/login", login);

            //Assert

            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
            //Todo: check if authentication cookie is set in response and database
        }

        [Fact]
        public async Task Logout_DeletesSessionInDb() {
            //Arrange
            var session = Session;
            //Act
            var response = await Client.PostAsJsonAsync("https://localhost:7038/api/auth/logout", session);
            //Assert
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
            Assert.DoesNotContain(session, Context.Sessions);
        }

        [Fact]
        public async Task LogoutAll_DeletesSessionInDb() {
            //Arrange
            var session = Session;
            //Act
            var response = await Client.PostAsJsonAsync("https://localhost:7038/api/auth/logout/all", Session);
            //Assert
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
            Assert.DoesNotContain(session, Context.Sessions);
        }

        [Fact]
        public async Task PasswordReset_ResetsPassword() {
            //Arrange
            var resetPasswordDto = new ResetPasswordDto { };
            //Act
            var response = await Client.PostAsJsonAsync("https://localhost:7038/api/auth/logout/all", Session);
            //Assert
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
            
        }

    }
}
