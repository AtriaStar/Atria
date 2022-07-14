using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;

namespace Atria.Controllers
{

    [ApiController]
    [Route("api/")]
    public class AuthenticationController : ControllerBase
    {
        private IConfiguration _config;

        public AuthenticationController(IConfiguration config)
        {
            _config = config;
        }


        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login()
        {
            return NotFound("User not found");
        }


    }
}
