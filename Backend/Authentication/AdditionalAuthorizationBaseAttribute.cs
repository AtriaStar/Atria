using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Models;

namespace Backend.Authentication;

/// <summary>
/// Abstract base class for advanced authorization attributes verifying additional user requirements.
/// </summary>
/// <para>
/// E.g. Extracts the authenticated <see cref="User"/> from the controller method parameters.
/// </para>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
public abstract class AdditionalAuthorizationBaseAttribute : Attribute, IActionFilter, IOrderedFilter {
    public int Order => 5;

    public abstract bool Authorize(User user, ActionExecutingContext context);

    public void OnActionExecuting(ActionExecutingContext context) {
        var authParam = context
            .GetParametersWithAttribute<FromAuthenticationAttribute>()
            .FirstOrDefault();
        if (authParam == null) {
            throw new InvalidOperationException("Missing authentication parameter for RequiresWseRightsAttribute!");
        }
        var user = context.ActionArguments[authParam.Name!] switch {
            User usr => usr,
            Session session => session.User,
            _ => throw new InvalidOperationException("Invalid authentication parameter"),
        };

        if (!Authorize(user, context)) {
            context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
        }
    }

    public void OnActionExecuted(ActionExecutedContext context) { }
}
