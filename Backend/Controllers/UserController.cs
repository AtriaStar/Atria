using Backend.AspPlugins;
using Backend.ParameterHelpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Backend.Controllers;

[ApiController]
[Route("user")]
public class UserController : ControllerBase {

    [HttpGet("{userId:long}")]
    public User Get([FromDatabase] User user) => user;

    [HttpGet("{userId:long}/wse")]
    public IQueryable<WebserviceEntry> GetWseByUser([FromServices] AtriaContext db, long userId)
        => db.WebserviceEntries.Where(x => x.Collaborators.Any(y => y.UserId == userId));

    [HttpGet("{userId:long}/bookmarks")]
    public ISet<WebserviceEntry> GetBookmarksByUser([FromDatabase] User user)
        => user.Bookmarks;

    [HttpGet("{userId:long}/reviews")]
    public IQueryable<Review> GetReviewsByUser(long userId, string query) => null!;

    [HttpGet("{userId:long}/drafts")]
    public IQueryable<WseDraft> GetWseDrafts() => null!;

    [HttpGet("{userId:long}/notifications")]
    public IReadOnlyList<Notification> GetNotifications() => null!;

    [HttpPost]
    public async Task<IActionResult> Edit([FromServices] AtriaContext db, User user) {
        var existingUser = await db.Users.FirstOrDefaultAsync(x => x.Id == user.Id);
        
        if (existingUser == null) { return NotFound(); }

        //existingUser.Email = user.Email;
        existingUser.Biography = user.Biography;
        existingUser.FirstNames = user.FirstNames;
        existingUser.ProfilePicture = user.ProfilePicture;
        existingUser.Title = user.Title;
        
        await db.SaveChangesAsync();
        return Ok(existingUser);
    }

    [HttpPost("{userId:long}/bookmarks")]
    public async Task SetBookmark([FromServices] AtriaContext db, [FromDatabase] User user, [FromDatabase] WebserviceEntry wse) {
        user.Bookmarks.Add(wse);
        await db.SaveChangesAsync();
    }

    [HttpDelete("{userId:long}")]
    public async Task Delete([FromServices] AtriaContext db, [FromDatabase] User user) {
        db.Users.Remove(user);
        await db.SaveChangesAsync();
    }
}
