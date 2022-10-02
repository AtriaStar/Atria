using Backend.AspPlugins;
using Backend.Authentication;
using Backend.ParameterHelpers;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Backend.Controllers;

[ApiController]
[Route("user")]
public class UserController : AtriaControllerBase {
    private readonly AtriaContext _context;

    public UserController(AtriaContext context) {
        _context = context;
    }

    [HttpGet("{userId:long}")]
    public User Get([FromDatabase] User user) => user;

    [HttpGet("{userId:long}/wse")]
    public IQueryable<WebserviceEntry> GetWseByUser(long userId, [FromQuery] Pagination pagination)
        => _context.WebserviceEntries.Where(x => x.Collaborators.Any(y => y.UserId == userId))
            .Paginate(pagination);

    [HttpGet("{userId:long}/bookmarks")]
    public IEnumerable<WebserviceEntry> GetBookmarksByUser([FromDatabase, Include(nameof(Models.User.Bookmarks))] User user, [FromQuery] Pagination pagination)
        => user.Bookmarks.Paginate(pagination);

    [HttpGet("{userId:long}/reviews")]
    public IQueryable<Review> GetReviewsByUser(long userId, [FromQuery] Pagination pagination)
        => _context.Reviews.Where(x => x.CreatorId == userId).Paginate(pagination);

    [RequiresAuthentication]
    [HttpPost]
    public async Task<IActionResult> Edit(User user, [FromAuthentication] User authUser) {
        if (user.Id != authUser.Id) { return Forbidden("You can only edit your own profile"); }

        if (user.SignUpIp != authUser.SignUpIp) { return BadRequest("Signup ip cannot be modified"); }
        if (user.Rights != authUser.Rights) { return BadRequest("Rights cannot be modified"); }
        if (user.CreatedAt != authUser.CreatedAt) { return BadRequest("Creation timestamp cannot be modified"); }

        user.PasswordHash = authUser.PasswordHash;
        user.PasswordSalt = authUser.PasswordSalt;

        // TODO: Extra endpoint for email
        user.Bookmarks = authUser.Bookmarks;

        _context.ChangeTracker.Clear(); // TODO: WHY IS THIS NEEDED??
        _context.Update(user);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpPost("{userId:long}/bookmarks/add/{wseId:long}")]
    public async Task AddBookmark([FromDatabase] User user, [FromDatabase] WebserviceEntry wse) {
        user.Bookmarks.Add(wse);
        _context.Update(user);
        await _context.SaveChangesAsync();
    }

    [HttpPost("{userId:long}/bookmarks/remove/{wseId:long}")]
    public async Task RemoveBookmark([FromDatabase, Include(nameof(Models.User.Bookmarks))] User user, [FromDatabase] WebserviceEntry wse) {
        user.Bookmarks.Remove(wse);
        _context.Update(user);
        await _context.SaveChangesAsync();
    }

    [RequiresAuthentication]
    [HttpDelete]
    public async Task Delete([FromAuthentication] User user) {
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }
}
