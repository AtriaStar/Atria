using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Backend.ParameterHelpers;

/// <summary>
/// Global filter handling the null value handling for <see cref="FromDatabaseAttribute"/>.
/// </summary>
public class DatabaseBinderNullFilter : IActionFilter {
    // TODO: I fucking hate this, maybe reinvestigate?
    public static object NullFromDatabase { get; } = new();

    public void OnActionExecuting(ActionExecutingContext context) {
        if (context.GetParametersWithAttribute<FromDatabaseAttribute>()
            .Any(x => context.ActionArguments[x.Name!] == NullFromDatabase)) {
            context.Result = new NotFoundResult();
        }
    }

    public void OnActionExecuted(ActionExecutedContext context) { }
}
