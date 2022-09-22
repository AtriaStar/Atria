using IntegrationTests.BaseTestClasses;
using IntegrationTests.Helpers;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace IntegrationTests {
    public class SearchControllerTests : AuthenticatedUserTests {
        public SearchControllerTests(CustomWebApplicationFactory<Program> factory) : base(factory) { }

        [Fact]
        public async Task GetWseList_ContainsSpecificWse() {
            //Arrange
            var wse = await Context.WebserviceEntries.FirstAsync();
            var parameters = new WseSearchParameters {
                Ascending = true,
                Query = wse.Name,
            };
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(parameters);
            //Act
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"https://localhost:7038/api/search/wse");
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");
            
            var response = await Client.SendAsync(request);
            WebserviceEntry[] wseArray = await response.Content.ReadFromJsonAsync<WebserviceEntry[]>();

            //Assert
            Assert.NotNull(wseArray.FirstOrDefault(x => x.Name.Equals(wse.Name)));
        }

        [Fact]
        public async Task GetUserList_OkStatusCode() {
            //Act
            var response = await Client.GetAsync("https://localhost:7038/api/search/user?type=user&query=a");
            
            //Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
