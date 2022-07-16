using Atria.Models;
using Microsoft.AspNetCore.Mvc;

namespace Atria.Controllers;

[ApiController]
[Route("/user")]
public class UserController : ControllerBase {

    //not in doc

    [HttpGet]
    public IReadOnlyList<User> GetUsers() => null!;

    [HttpGet("/by_title/{title}")]
    public IEnumerable<User> GetUsersByTitle(string title) => null!;

    [HttpPost("/add")]
    public void AddUser(User user) { }

    //not in doc

    [HttpGet("/{userId}")]
    public User Get(int userId) => null!;

    //WseSearchParam and Pagination missing in param
    [HttpGet("/{userId}/wse")]
    public IEnumerable<WebserviceEntry> GetWseByUser(int userId) => null!;

    //WseSearchParam and Pagination missing in param
    [HttpGet("/{userId}/bookmarks")]
    public IEnumerable<WebserviceEntry> GetBookmarksByUser(int userId) => null!;

    //Pagination missing in param
    [HttpGet("/{userId}/reviews")]
    public IEnumerable<Review> GetReviewsByUser(int userId, string query) => null!;

    //WseSearchParam and Pagination missing in param
    [HttpGet("/{userId}/drafts")]
    public IEnumerable<WSEDraft> GetWseDrafts() => null!;

    [HttpGet("/{userId}/notifications")]
    public IReadOnlyList<Notification> GetNotifications() => null!;

    [HttpPost("/{userId}")]
    public void Edit(User user) { }

    [HttpPost("/{userId}/bookmarks")]
    public void SetBookmark(int wseId, bool state) { }

    [HttpPut("")]
    public int Create(User user) => 0;

    [HttpDelete("/{userId}")]
    public void Delete(int userId) { }
}
