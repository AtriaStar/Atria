using System.Reflection;
using Backend.AspPlugins;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;

namespace Backend; 

public static class Extensions {
    public static bool IsNullable(this Type t)
        => Nullable.GetUnderlyingType(t) != null;


    public static MethodInfo? GetMethod(this ActionExecutingContext context)
        => (context.ActionDescriptor as ControllerActionDescriptor)?.MethodInfo;

    public static IEnumerable<ParameterInfo> GetBasicParameters(this ActionExecutingContext context)
        => context.GetMethod()?.GetParameters()
            .Where(x => x.Name != null)
            ?? Enumerable.Empty<ParameterInfo>();

    public static IEnumerable<ParameterInfo> GetParametersWithAttribute<T>(this ActionExecutingContext context)
        where T : Attribute
        => context.GetBasicParameters().Where(x => x.GetCustomAttributes(typeof(T)).Any());


    public static void UseCentralRoutePrefix(this MvcOptions opt, IRouteTemplateProvider routeAttribute) {
        opt.Conventions.Insert(0, new RouteConvention(routeAttribute));
    }
}
