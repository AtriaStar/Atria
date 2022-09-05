using Backend.AspPlugins;
using Backend.Authentication;
using Backend.ParameterHelpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Backend.Controllers;

[ApiController]
[Route("user")]
public class UserController : ControllerBase
{
    private readonly AtriaContext _context;

    public UserController(AtriaContext context)
    {
        _context = context;
    }

    [HttpGet("{userId:long}")]
    public User Get([FromDatabase] User user) => user;

    [HttpGet("{userId:long}/wse")]
    public IQueryable<WebserviceEntry> GetWseByUser(long userId)
        => _context.WebserviceEntries.Where(x => x.Collaborators.Any(y => y.UserId == userId));

    [HttpGet("{userId:long}/bookmarks")]
    public ISet<WebserviceEntry> GetBookmarksByUser([FromDatabase] User user)
        => user.Bookmarks;

    [HttpGet("{userId:long}/reviews")]
    public IQueryable<Review> GetReviewsByUser(long userId)
        => _context.Reviews.Where(x => x.CreatorId == userId);

    [RequiresAuthentication]
    [HttpGet("{userId:long}/drafts")]
    public ICollection<WseDraft> GetWseDrafts([FromDatabase] User user)
        => user.WseDrafts;

    [HttpGet("{userId:long}/notifications")]
    public IReadOnlyList<Notification> GetNotifications() => null!;

    [RequiresAuthentication]
    [HttpPost]
    public async Task<IActionResult> Edit(User user, [FromAuthentication] User authUser)
    {
        if (user.Id != authUser.Id) { return Forbid("You can only edit your own profile"); }

        if (user.SignUpIp != authUser.SignUpIp) { return BadRequest("Signup ip cannot be modified"); }
        if (user.Rights != authUser.Rights) { return BadRequest("Rights cannot be modified"); }
        if (user.CreatedAt != authUser.CreatedAt) { return BadRequest("Creation timestamp cannot be modified"); }

        user.PasswordHash = authUser.PasswordHash;
        user.PasswordSalt = authUser.PasswordSalt;

        // TODO: Extra endpoint for email
        user.WseDrafts = authUser.WseDrafts;
        user.Bookmarks = authUser.Bookmarks;

        _context.Update(user);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpPost("{userId:long}/bookmarks/add/{wseId:long}")]
    public async Task AddBookmark([FromDatabase] User user, [FromDatabase] WebserviceEntry wse)
    {
        user.Bookmarks.Add(wse);
        _context.Update(user);
        await _context.SaveChangesAsync();
    }

    [HttpPost("{userId:long}/bookmarks/remove/{wseId:long}")]
    public async Task RemoveBookmark([FromDatabase] User user, [FromDatabase] WebserviceEntry wse)
    {
        user.Bookmarks.Remove(wse);
        _context.Update(user);
        await _context.SaveChangesAsync();
    }

    [RequiresAuthentication]
    [HttpDelete]
    public async Task Delete([FromAuthentication] User user)
    {
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }
}
