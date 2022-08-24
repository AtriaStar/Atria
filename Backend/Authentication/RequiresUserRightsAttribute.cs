using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Models;

namespace Backend.Authentication; 

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
public class RequiresUserRightsAttribute : Attribute, IActionFilter, IOrderedFilter {
    public UserRights UserRights { get; }

    public RequiresUserRightsAttribute(UserRights rights) {
        UserRights = rights;
    }


    public int Order => 5;

    public void OnActionExecuting(ActionExecutingContext context) {
        var auth = context.ActionArguments[context.GetParametersWithAttribute<FromAuthenticationAttribute>().First().Name!]!;
        var type = auth.GetType();
        User user;
        if (type == typeof(User)) {
            user = (User)auth;
        } else if (type == typeof(Session)) {
            user = ((Session)auth).User;
        } else {
            throw new InvalidOperationException("Missing RequiresAuthenticationAttribute");
        }

        if ((user.Rights & UserRights) != UserRights) {
            context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
        }
    }

    public void OnActionExecuted(ActionExecutedContext context) { }
}
    public sealed override bool Authorize(User user, ActionExecutingContext context) => (user.Rights & UserRights) == UserRights;
}

