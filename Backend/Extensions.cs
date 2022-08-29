using System.Reflection;
using Backend.AspPlugins;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;
using Models;

namespace Backend; 

public static class Extensions {
    public static IEnumerable<T> Paginate<T>(this IEnumerable<T> queryable, Pagination pagination)
        => queryable.Skip(pagination.Page * pagination.EntriesPerPage)
                    .Take(pagination.EntriesPerPage);

    public static IEnumerable<ParameterInfo> GetBasicParameters(this ActionExecutingContext context)
        => (context.ActionDescriptor as ControllerActionDescriptor)?.MethodInfo.GetParameters()
            .Where(x => x.Name != null)
            ?? Enumerable.Empty<ParameterInfo>();

    public static IEnumerable<ParameterInfo> GetParametersWithAttribute<T>(this ActionExecutingContext context)
        where T : Attribute
        => context.GetBasicParameters().Where(x => x.GetCustomAttributes(typeof(T)).Any());


    public static void UseCentralRoutePrefix(this MvcOptions opt, IRouteTemplateProvider routeAttribute) {
        opt.Conventions.Insert(0, new RouteConvention(routeAttribute));
    }
}
