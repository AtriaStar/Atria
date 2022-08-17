using Microsoft.AspNetCore.Mvc.Filters;
using Models;

namespace Backend.Authentication; 

public class RequiresWseRightsAttribute : IAsyncActionFilter {
    public WseRights WseRights { get; }

    public RequiresWseRightsAttribute(WseRights rights) {
        WseRights = rights;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next) {
        //context.GetBasicParameters().Where(x => x.ParameterType == typeof(WebserviceEntry)) 
    }
}
