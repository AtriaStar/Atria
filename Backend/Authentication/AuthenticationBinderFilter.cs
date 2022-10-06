using System.Reflection;
using Backend.AspPlugins;
using Backend.ParameterHelpers;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Backend.Authentication;

/// <summary>
/// Fills authorization parameters with the authenticated user from the session cookie, fails the call if session cookie invalid or absent.
/// </summary>
public class AuthenticationBinderFilter : IAsyncActionFilter, IOrderedFilter {
    private static readonly NullabilityInfoContext Nullability = new();

    public int Order => 0;

    private async Task<bool> Execute(ActionExecutingContext context) {
        var authParams = context.GetParametersWithAttribute<FromAuthenticationAttribute>().ToArray();
        var optional = authParams.All(x => Nullability.Create(x).ReadState == NullabilityState.Nullable)
                       && context.GetMethod()?.GetCustomAttribute<RequiresAuthenticationAttribute>() == null;
        if (!authParams.Any() && optional) {
            return true;
        }

        var services = context.HttpContext.RequestServices;
        var db = services.GetRequiredService<AtriaContext>();
        var ss = services.GetRequiredService<SessionService>();
        Session? session;
        if (!context.HttpContext.Request.Cookies.TryGetValue(ss.AuthorizationCookieName, out var token)
            || (session = await db.Sessions
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Token == token)) == null) {
            return RejectIfMandatory();
        }

        if (!ss.IsValid(session)) {
            await ss.DeleteSession(session, db, context.HttpContext.Response);
            return RejectIfMandatory();
        }

        bool RejectIfMandatory() {
            if (!optional) {
                context.Result = new UnauthorizedResult();
            }
            return optional;
        }

        foreach (var p in authParams) {
            object entity;
            if (p.ParameterType == typeof(Session)) {
                entity = session;
            } else if (p.ParameterType == typeof(User)) {
                entity = session.User;
            } else {
                throw new ArgumentOutOfRangeException(p.Name, p.ParameterType, "Must be Session or User");
            }
            context.ActionArguments[p.Name!] = entity;

            await p.GetCustomAttributes<IncludeAttribute>()
                .ApplyToAsync(db, entity);
        }

        return true;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next) {
        if (await Execute(context)) {
            await next();
        }
    }
}
