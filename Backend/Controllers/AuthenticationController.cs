using Backend.AspPlugins;
using Backend.Authentication;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.DTO;

namespace Backend.Controllers;

[ApiController]
[Route("auth")]
public class AuthenticationController : ControllerBase {
    private readonly AtriaContext _context;

    public AuthenticationController(AtriaContext context) {
        _context = context;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(Registration registration, [FromServices] SessionService ss) {
        if (await _context.Users.AnyAsync(x => x.Email == registration.Email)) {
            return Conflict("Email is already taken");
        }

        var salt = HashingService.GenerateSalt();

        var user = new User {
            FirstNames = registration.FirstNames,
            LastName = registration.LastName,
            Email = registration.Email,
            SignUpIp = HttpContext.Connection.RemoteIpAddress!.ToString(),
            PasswordHash = HashingService.Hash(registration.Password, salt),
            PasswordSalt = salt,
        };

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        await ss.GenerateSession(user, _context, HttpContext);

        return Ok();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(Login login, [FromServices] SessionService ss) {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == login.Email);
        if (user == null || !user.PasswordHash.SequenceEqual(HashingService.Hash(login.Password, user.PasswordSalt))) {
            return Unauthorized("Email or password invalid");
        }
        await ss.GenerateSession(user, _context, HttpContext);
        return Ok();
    }

    [RequiresAuthentication]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout([FromAuthentication] Session session, [FromServices] SessionService ss) {
        await ss.DeleteSession(session, _context, Response);
        return Ok();

    }

    [RequiresAuthentication]
    [HttpPost("logout/all")]
    public async Task<IActionResult> LogoutAll([FromAuthentication] Session session, [FromServices] SessionService ss) {
        await ss.DeleteSession(session, _context, Response);
        _context.Sessions.RemoveRange(_context.Sessions.Where(x => x.User == session.User));
        await _context.SaveChangesAsync();
        return Ok();
    }

    [RequiresAuthentication] 
    [HttpGet("sessions")]
    public IQueryable<Session> GetSessions([FromAuthentication] User user)
        => _context.Sessions
            .Include(x => x.User)
            .Where(x => x.User == user);

    [RequiresAuthentication]
    [HttpGet("")]
    public User GetAuthUser([FromAuthentication] User user) => user;

}
