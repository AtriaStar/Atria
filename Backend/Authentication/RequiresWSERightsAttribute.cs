using Backend.ParameterHelpers;
using Microsoft.AspNetCore.Mvc.Filters;
using Models;

namespace Backend.Authentication;

public class RequiresWseRightsAttribute : AdditionalAuthorizationBaseAttribute {
    public WseRights WseRights { get; }

    public RequiresWseRightsAttribute(WseRights rights) {
        WseRights = rights;
    }

    public sealed override bool Authorize(User user, ActionExecutingContext context) {
        var wseParam = context
            .GetParametersWithAttribute<FromDatabaseAttribute>()
            .FirstOrDefault(x => x.ParameterType == typeof(WebserviceEntry));
        if (wseParam == null) {
            throw new InvalidOperationException("Missing WSE parameter for RequiresWseRightsAttribute!");
        }
        var wse = (WebserviceEntry)context.ActionArguments[wseParam.Name!]!;
        
        var collaborator = wse.Collaborators.FirstOrDefault(x => x.User == user);
        return collaborator != null && (collaborator.Rights & WseRights) == WseRights;
    }
}
