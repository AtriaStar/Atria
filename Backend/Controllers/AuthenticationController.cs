using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers; 

[ApiController]
public class AuthenticationController : ControllerBase {
    private IConfiguration _config;

    public AuthenticationController(IConfiguration config) {
        _config = config;
    }


    [AllowAnonymous]
    [HttpPost("login")]
    public IActionResult Login() {
        return NotFound("User not found");
    }
}
