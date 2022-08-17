using Microsoft.AspNetCore.Mvc;
using Models;

namespace Backend; 

public abstract class AuthorizationSupportedController : Controller {
    protected Session GetAuthenticationSession()
        => (Session)ViewData["session"]!;

    protected User GetAuthenticatedUser()
        => (User)ViewData["user"]!;
}
