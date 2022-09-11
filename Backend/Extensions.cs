using System.Reflection;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Backend; 

public static class Extensions {
    public static bool OlderThan(this DateTimeOffset time, TimeSpan duration)
        => DateTimeOffset.UtcNow - time > duration;

    public static MethodInfo? GetMethod(this ActionExecutingContext context)
        => (context.ActionDescriptor as ControllerActionDescriptor)?.MethodInfo;

    public static IEnumerable<ParameterInfo> GetBasicParameters(this ActionExecutingContext context)
        => context.GetMethod()?.GetParameters()
            .Where(x => x.Name != null)
            ?? Enumerable.Empty<ParameterInfo>();

    public static IEnumerable<ParameterInfo> GetParametersWithAttribute<T>(this ActionExecutingContext context)
        where T : Attribute
        => context.GetBasicParameters().Where(x => x.GetCustomAttributes(typeof(T)).Any());
}
