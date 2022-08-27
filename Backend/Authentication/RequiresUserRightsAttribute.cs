using Microsoft.AspNetCore.Mvc.Filters;
using Models;

namespace Backend.Authentication; 

public class RequiresUserRightsAttribute : AdditionalAuthorizationBaseAttribute {
    public UserRights UserRights { get; }

    public RequiresUserRightsAttribute(UserRights rights) {
        UserRights = rights;
    }

    public sealed override bool Authorize(User user, ActionExecutingContext context) => (user.Rights & UserRights) == UserRights;
}
