using Atria.Models;
using Microsoft.AspNetCore.Mvc;

namespace Atria.Controllers;

[ApiController]
[Route("api/users/")]
public class UserController : ControllerBase {
    [HttpGet]
    public IReadOnlyList<User> GetUsers() => null!;

    [HttpGet("by_title/{title}")]
    public IEnumerable<User> GetUsersByTitle(string title)
        => null!;

    [HttpPost("add")]
    public void AddUser(User user) { }
}
