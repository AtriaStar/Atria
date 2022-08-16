using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Backend.Controllers; 

[ApiController]
public class AuthenticationController : ControllerBase {
    private readonly IConfiguration _config;
    private readonly AtriaContext _context;

    public AuthenticationController(IConfiguration config, AtriaContext context) {
        _config = config;
        _context = context;
    }

    [HttpPost("register")]
    public IActionResult Register(Registration registration) {
        var salt = HashingService.GenerateSalt();

        _context.Users.Add(new() {
            FirstNames = registration.FirstNames,
            LastName = registration.LastName,
            Email = registration.Email,
            PasswordHash = HashingService.Hash(registration.Password, salt),
            PasswordSalt = salt,
        });
        try {
            _context.SaveChanges();
        } catch (DbUpdateException) {
            return BadRequest();
        }

        return Ok();
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public IActionResult Login(Login login) {
        var user = _context.Users.FirstOrDefault(x => x.Email == login.Email);
        return user != null && user.PasswordHash.SequenceEqual(HashingService.Hash(login.Password, user.PasswordSalt))
            ? Ok()
            : NotFound("Email or password invalid");
    }
}
