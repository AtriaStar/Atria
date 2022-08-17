using System.Reflection;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Backend.Authentication;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
public class RequiresAuthenticationAttribute : Attribute, IAsyncActionFilter {
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next) {
        var services = context.HttpContext.RequestServices;
        var db = services.GetRequiredService<AtriaContext>();
        var ss = services.GetRequiredService<SessionService>();
        Session? session;
        if (!context.HttpContext.Request.Cookies.TryGetValue(ss.AuthorizationCookieName, out var token)
            || (session = await db.Sessions
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Token == token)) == null) {
            Reject();
            return;
        }

        if (!ss.IsValid(session)) {
            await ss.DeleteSession(session, db, context.HttpContext.Response);
            Reject();
            return;
        }

        void Reject() {
            context.Result = new UnauthorizedObjectResult("This endpoint requires login");
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

        await next();
    }
}
