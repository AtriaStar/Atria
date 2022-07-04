using Atria.Models;
using Microsoft.AspNetCore.Mvc;

namespace Atria.Controllers;

[ApiController]
[Route("api/users/")]
public class UserController : ControllerBase {
    private readonly UserManager _mgr;

    public UserController(UserManager mgr) {
        _mgr = mgr;
    }

    [HttpGet]
    public IReadOnlyList<User> GetUsers() => _mgr.Users;

    [HttpGet("by_title/{title}")]
    public IEnumerable<User> GetUsersByTitle(string title)
        => _mgr.Users.Where(x => x.Title == title);

    [HttpPost("add")]
    public void AddUser(User user) => _mgr.Add(user);
}
