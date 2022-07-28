using Microsoft.AspNetCore.Mvc;
using Models;

namespace Backend.Controllers;

[ApiController]
[Route("user")]
public class UserController : ControllerBase {
    [HttpGet("{userId}")]
    public User Get(int userId) => null!;

    // TODO: WseSearchParam and Pagination missing in param
    [HttpGet("{userId}/wse")]
    public IEnumerable<WebserviceEntry> GetWseByUser(int userId) => null!;

    // TODO: WseSearchParam and Pagination missing in param
    [HttpGet("{userId}/bookmarks")]
    public IEnumerable<WebserviceEntry> GetBookmarksByUser(int userId) => null!;

    // TODO: Pagination missing in param
    [HttpGet("{userId}/reviews")]
    public IEnumerable<Review> GetReviewsByUser(int userId, string query) => null!;

    // TODO: WseSearchParam and Pagination missing in param
    [HttpGet("{userId}/drafts")]
    public IEnumerable<WSEDraft> GetWseDrafts() => null!;

    [HttpGet("{userId}/notifications")]
    public IReadOnlyList<Notification> GetNotifications() => null!;

    [HttpPost("{userId}")]
    public void Edit(User user) { }

    [HttpPost("{userId}/bookmarks")]
    public void SetBookmark(int wseId, bool state) { }

    [HttpPut("")]
    public int Create(User user) => 0;

    [HttpDelete("{userId}")]
    public void Delete(int userId) { }
}
