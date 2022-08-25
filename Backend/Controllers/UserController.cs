using Backend.ParameterHelpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Runtime.Intrinsics.X86;

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

    [HttpPost("")]
    public async Task<IActionResult> Edit([FromServices] AtriaContext db, User user) {
        User existingUser = db.Users.FirstOrDefault(x => x.Id == user.Id);
        
        if (existingUser == null) {
            return BadRequest();
        }

        existingUser.Email = user.Email;
        existingUser.Biography = user.Biography;
        existingUser.FirstNames = user.FirstNames;
        existingUser.ProfilePicture = user.ProfilePicture;
        existingUser.Title = user.Title;
        
        await db.SaveChangesAsync();
        return Ok(existingUser);
    }

    [HttpPost("{userId:long}/bookmarks")]
    public async Task<IActionResult> SetBookmark([FromServices] AtriaContext db, [FromDatabase] User user, [FromDatabase] WebserviceEntry wse) {
        user.Bookmarks.Add(wse);
        await db.SaveChangesAsync();
        return Ok();
    }

    [HttpPut("")]
    public async Task<IActionResult> Create([FromServices] AtriaContext db, User user) {
        await db.Users.AddAsync(user);
        await db.SaveChangesAsync();

        return CreatedAtAction(nameof(Get), new {userId = user.Id }, user);
    }


    [HttpDelete("{userId:long}")]
    public async Task<IActionResult> Delete([FromServices] AtriaContext db, [FromDatabase] User user) {
        db.Users.Remove(user);
        await db.SaveChangesAsync();
        return Ok(user);
    }
}
