using System.Security.Cryptography;
using Backend.Authentication;
using Backend.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Backend.Controllers;

[ApiController]
public class AuthenticationController : ControllerBase {
    // TODO: Turn into options
    private const string AUTH_COOKIE_NAME = "Authorization";
    private const int SESSIONS_EXPIRE_DAYS = 7;

    private readonly AtriaContext _context;

    public AuthenticationController(AtriaContext context) {
        _context = context;
    }

    private void AuthenticateClient(User user) {
        var token = Base64UrlTextEncoder.Encode(RandomNumberGenerator.GetBytes(64));

        _context.Sessions.Add(new() {
            Token = token,
            User = user,
            Ip = HttpContext.Connection.RemoteIpAddress!.ToString(),
            UserAgent = Request.Headers["User-Agent"],
            CreatedAt = DateTimeOffset.UtcNow,
        });
        _context.SaveChanges();

        Response.Cookies.Append(AUTH_COOKIE_NAME, token, new() {
            IsEssential = true,
            HttpOnly = true,
            Secure = true,
            Domain = "localhost",
            MaxAge = TimeSpan.FromDays(SESSIONS_EXPIRE_DAYS),
            SameSite = SameSiteMode.None, // TODO: Reinvestigate, I fear this is necessary
            //Path = "/api/", // TODO: Implement
        });
    }

    [HttpPost("register")]
    public IActionResult Register(Registration registration) {
        var salt = HashingService.GenerateSalt();

        var user = new User {
            FirstNames = registration.FirstNames,
            LastName = registration.LastName,
            Email = registration.Email,
            SignUpIp = HttpContext.Connection.RemoteIpAddress!.ToString(),
            PasswordHash = HashingService.Hash(registration.Password, salt),
            PasswordSalt = salt,
        };
        _context.Users.Add(user);
        try {
            _context.SaveChanges();
        } catch (DbUpdateException) {
            return BadRequest();
        }
        AuthenticateClient(user);

        return Ok();
    }

    [HttpPost("login")]
    public IActionResult Login(Login login) {
        var user = _context.Users.FirstOrDefault(x => x.Email == login.Email);
        if (user == null || !user.PasswordHash.SequenceEqual(HashingService.Hash(login.Password, user.PasswordSalt))) {
            return Unauthorized("Email or password invalid");
        }
        AuthenticateClient(user);
        return Ok();
    }

    [RequiresAuthentication]
    [HttpPost("logout")]
    public IActionResult Logout([FromAuthentication] Session session) {
        Response.Cookies.Delete(AUTH_COOKIE_NAME);
        _context.Sessions.Remove(session);
        _context.SaveChanges();
        return Ok();
    }

    [RequiresAuthentication]
    [HttpPost("logout/all")]
    public IActionResult LogoutAll([FromAuthentication] Session session) {
        Logout(session);
        _context.Sessions.RemoveRange(_context.Sessions.Where(x => x.User == session.User));
        _context.SaveChanges();
        return Ok();
    }

    [RequiresAuthentication]
    [HttpGet("sessions")]
    public IEnumerable<Session> GetSessions([FromAuthentication] User user)
        => _context.Sessions
            .Include(x => x.User)
            .Where(x => x.User == user);
}
