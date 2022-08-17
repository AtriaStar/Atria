using Backend.Authentication;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Backend.Controllers;

[ApiController]
public class AuthenticationController : ControllerBase {
    private readonly AtriaContext _context;
    private readonly SessionService _sessionService;

    public AuthenticationController(AtriaContext context, SessionService sessionService) {
        _context = context;
        _sessionService = sessionService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(Registration registration, [FromServices] SessionService ss) {
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
        try {
            await _context.SaveChangesAsync();
        } catch (DbUpdateException) {
            return BadRequest();
        }
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
    public IEnumerable<Session> GetSessions([FromAuthentication] User user)
        => _context.Sessions
            .Include(x => x.User)
            .Where(x => x.User == user);
}
