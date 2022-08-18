using Backend.ParameterHelpers;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Backend.Controllers;

[ApiController]
[Route("user")]
public class UserController : ControllerBase {
    [HttpGet("{userId:long}")]
    public User Get([FromDatabase] User user) => user;

    [HttpGet("{userId:long}/wse")]
    public IEnumerable<WebserviceEntry> GetWseByUser([FromServices] AtriaContext db, long userId)
        => db.WebserviceEntries.Where(x => x.Collaborators.Any(y => y.UserId == userId));

    [HttpGet("{userId:long}/bookmarks")]
    public IEnumerable<WebserviceEntry> GetBookmarksByUser([FromDatabase] User user)
        => user.Bookmarks;

    [HttpGet("{userId:long}/reviews")]
    public IEnumerable<Review> GetReviewsByUser(long userId, string query) => null!;

    [HttpGet("{userId:long}/drafts")]
    public IEnumerable<WSEDraft> GetWseDrafts() => null!;

    [HttpGet("{userId:long}/notifications")]
    public IReadOnlyList<Notification> GetNotifications() => null!;

    [HttpPost("{userId:long}")]
    public void Edit(User user) { }

    [HttpPost("{userId:long}/bookmarks")]
    public void SetBookmark(int wseId, bool state) { }

    [HttpPut("")]
    public int Create(User user) => 0;

    [HttpDelete("{userId:long}")]
    public void Delete(int userId) { }
}
