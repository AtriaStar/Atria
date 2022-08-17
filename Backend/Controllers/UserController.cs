using Backend.Authentication;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Backend.Controllers;

[ApiController]
[Route("user")]
public class UserController : ControllerBase {
    [HttpGet("{user}")]
    public User Get([FromDatabase] User user) => user;

    [HttpGet("{userId:long}/wse")]
    public IEnumerable<WebserviceEntry> GetWseByUser([FromServices] AtriaContext db, long userId)
        => db.WebserviceEntries.Where(x => x.Collaborators.Any(y => y.UserId == userId));

    [HttpGet("{user}/bookmarks")]
    public IEnumerable<WebserviceEntry> GetBookmarksByUser([FromDatabase] User user)
        => user.Bookmarks;

    [HttpGet("{userId:long}/reviews")]
    public IEnumerable<Review> GetReviewsByUser(long userId, string query) => null!;

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
