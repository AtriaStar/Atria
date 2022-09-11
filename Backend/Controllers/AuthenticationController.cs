using System.Security.Cryptography;
using Backend.AspPlugins;
using Backend.Authentication;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
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
    public async Task<IActionResult> Register(RegistrationDto registrationDto, [FromServices] SessionService ss, [FromServices] BackendSettings opt) {
        if (await _context.Users.AnyAsync(x => x.Email == registrationDto.Email)) {
            return Conflict("Email is already taken");
        }

        var salt = HashingService.GenerateSalt();

        var user = new User {
            FirstNames = registrationDto.FirstNames,
            LastName = registrationDto.LastName,
            Email = registrationDto.Email,
            SignUpIp = HttpContext.Connection.RemoteIpAddress!.ToString(),
            PasswordHash = HashingService.Hash(registrationDto.Password, salt),
            PasswordSalt = salt,
        };

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        await ss.GenerateSession(user, _context, HttpContext);

        return Ok();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto loginDto, [FromServices] SessionService ss, [FromServices] BackendSettings opt) {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == loginDto.Email);
        if (user == null || !user.PasswordHash.SequenceEqual(HashingService.Hash(loginDto.Password, user.PasswordSalt))) {
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
    [HttpGet]
    public User GetAuthUser([FromAuthentication] User user) => user;

    [HttpPost("reset/start")]
    public async Task InitPasswordReset([FromBody] string email) {
        email = email.ToLowerInvariant();
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
        if (user == null) { return; }

        var token = RandomNumberGenerator.GetBytes(64);
        await _context.ResetTokens.AddAsync(new() {
            User = user,
            Token = token,
        });
        await _context.SaveChangesAsync();

        var textToken = Base64UrlTextEncoder.Encode(token);
        // TODO: Send via email
    }
    
    [HttpPost("reset/finish")]
    public async Task<IActionResult> PasswordReset(ResetPasswordDto dto, [FromServices] BackendSettings opt) {
        var token = Base64UrlTextEncoder.Decode(dto.Token);
        var resetToken = await _context.ResetTokens.FirstOrDefaultAsync(x => x.Token.SequenceEqual(token));
        if (resetToken == null) { return BadRequest("Invalid token"); }
        if (resetToken.CreatedAt.OlderThan(opt.ResetTokenExpireDuration)) { return BadRequest("Token expired"); }
        var user = resetToken.User;
        ChangePassword(user, dto.Password);
        _context.ResetTokens.Remove(resetToken);
        _context.Sessions.RemoveRange(_context.Sessions.Where(x => x.User == user));
        await _context.SaveChangesAsync();
        return Ok();
    }

    [RequiresAuthentication]
    [HttpPost("change-password-uwu")]
    public async Task PasswordChange([FromAuthentication] User user, string newPassword) {
        ChangePassword(user, newPassword);
        _context.Update(user);
        await _context.SaveChangesAsync();
    }

    private static void ChangePassword(User user, string newPassword) {
        user.PasswordSalt = HashingService.GenerateSalt();
        user.PasswordHash = HashingService.Hash(newPassword, user.PasswordSalt);
    }
}
