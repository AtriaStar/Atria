using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Backend.Authentication;

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

        foreach (var p in (context.ActionDescriptor as ControllerActionDescriptor)?.MethodInfo.GetParameters()
                          .Where(x => x.Name != null && x.CustomAttributes
                              .Any(y => y.AttributeType == typeof(FromAuthenticationAttribute)))
                          ?? Enumerable.Empty<ParameterInfo>()) {
            if (p.ParameterType == typeof(Session)) {
                context.ActionArguments[p.Name!] = session;
            } else if (p.ParameterType == typeof(User)) {
                context.ActionArguments[p.Name!] = session.User;
            }
        }
    }
}
