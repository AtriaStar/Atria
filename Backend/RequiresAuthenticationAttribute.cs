using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace Backend; 

public class RequiresAuthenticationAttribute : ActionFilterAttribute {
    public override void OnActionExecuting(ActionExecutingContext context) {
        var db = context.HttpContext.RequestServices.GetService<AtriaContext>()!;
        Session? session;
        if (!context.HttpContext.Request.Cookies.TryGetValue("Authorization", out var token)
            || (session = db.Sessions
                .Include(x => x.User)
                .FirstOrDefault(x => x.Token == token)) == null) {
            context.Result = new UnauthorizedObjectResult("This endpoint requires login");
            return;
        }
        var data = ((Controller)context.Controller).ViewData;
        data.Add("session", session);
        data.Add("user", session.User);
    }
}
