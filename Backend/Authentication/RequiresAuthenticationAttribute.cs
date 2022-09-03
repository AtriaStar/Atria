using Backend.AspPlugins;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Backend.Authentication;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
public class RequiresAuthenticationAttribute : Attribute, IAsyncActionFilter, IOrderedFilter {
    public int Order => 0;

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next) {
        Console.Out.WriteLine("___________________________________________________");
        var services = context.HttpContext.RequestServices;
        var db = services.GetRequiredService<AtriaContext>();
        var ss = services.GetRequiredService<SessionService>();

        //debug
        Console.Out.WriteLine(context.HttpContext.Request.Cookies.Count);
        

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
            context.Result = new UnauthorizedResult();
        }

        foreach (var p in context.GetParametersWithAttribute<FromAuthenticationAttribute>()) {
            if (p.ParameterType == typeof(Session)) {
                context.ActionArguments[p.Name!] = session;
            } else if (p.ParameterType == typeof(User)) {
                context.ActionArguments[p.Name!] = session.User;
            }
        }

        await next();
    }
}
