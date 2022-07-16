using Atria.Models;
using Microsoft.AspNetCore.Mvc;

namespace Atria.Controllers;

[ApiController]
[Route("/search")]
public class SearchController : ControllerBase {

    // TODO: WseSearchParam and Pagination missing in param
    [HttpGet("/wse")]
    public IEnumerable<WebserviceEntry> GetWseList() => null!;

    // TODO: Pagination missing in param
    [HttpGet("/user")]
    public IEnumerable<User> GetUserList(string query) => null!;

    [HttpGet("/count/wse")]
    public int GetWseCount(string query) => 0!;

    [HttpGet("/count/user")]
    public int GetUserCount(string query) => 0!;
}
